using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views.Internals;
using Xamarin.Forms;

namespace XamsungHealth.Controls
{
	public class LabeledProgressBar : BaseTemplatedView<Grid>
	{
		#region Static properties
		static uint AnimationLength { get; } = 4000;
		static uint AnimationRate { get; } = 100;
		static Color mainGreen
		{
			get
			{
				Application.Current.Resources.TryGetValue("MainGreen", out object color);
				return (Color)color;
			}
		}
		#endregion

		#region Properties
		Label ProgressBarText { get; } = CreateTextElement();
		ProgressBar ProgressBar { get; } = CreateProgressBarElement();
		#endregion

		#region bindable properties
		public static readonly BindableProperty PercentageProperty = BindableProperty.Create(
												propertyName: nameof(Percentage),
												returnType: typeof(double),
												declaringType: typeof(LabeledProgressBar),
												defaultValue: 0d,
												propertyChanged: OnPercentageChanged,
												defaultBindingMode: BindingMode.TwoWay);
		public double Percentage
		{
			get { return (double)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, value); }
		}
		#endregion

		#region events handlers
		static async void OnPercentageChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var newValueDouble = (double)newValue;
			var oldValueDouble = (double)oldValue;

			var progressBar = (bindable as LabeledProgressBar).ProgressBar;
			var progressBarLabel = (bindable as LabeledProgressBar).ProgressBarText;

			await AnimateProgressAsync(progressBar, progressBarLabel, oldValueDouble / 100f, newValueDouble / 100f);
		}
		#endregion

		#region Static Methods
		public static Task AnimateProgressAsync(ProgressBar progressBar, Label label, double start, double end)
		{
			var tcs = new TaskCompletionSource<bool>();
			var tcs2 = new TaskCompletionSource<bool>();

			var progressBarAnimation = new Animation();
			progressBarAnimation.WithConcurrent(
							(f) => progressBar.Progress = f,
							start, end, Easing.Linear);

			var progressLabelUpdateAnimation = new Animation(); //Update text + Translate
			progressLabelUpdateAnimation.WithConcurrent(
				(f) => UpdateProgressText(label, progressBar.Width, f),
				start * 100f, end * 100f, Easing.Linear);

			progressBarAnimation.Commit(progressBar, nameof(AnimateProgressAsync), length: AnimationLength,
			   finished: (v, t) => tcs.SetResult(true));

			progressLabelUpdateAnimation.Commit(label, nameof(AnimateProgressAsync), length: AnimationLength,
				rate: AnimationRate, finished: (v, t) => tcs2.SetResult(true));

			return tcs.Task;
		}

		static double GetXTranslate(double progress, double progressBarWidth)
		{
			var translateX = progress * progressBarWidth;
			return translateX <= 30 ? 0 : translateX - 30;
		}

		static void UpdateProgressText(Label label, double progressBarWidth, double newValue)
		{
			if (newValue != 0)
			{
				label.Text = $"{Math.Floor(newValue)}%";
				label.TextColor = Color.White;
				label.FontAttributes = FontAttributes.Bold;
				label.TranslationX = GetXTranslate(newValue / 100f, progressBarWidth);
				label.HorizontalOptions = LayoutOptions.Fill;
			}
			else
			{
				label.Text = "Start walking!";
				label.TextColor = Color.Black;
				label.TranslationX = 0;
				label.FontAttributes = FontAttributes.None;
				label.HorizontalOptions = LayoutOptions.Center;
			}
		}

		static Label CreateTextElement()
			=> new()
			{
				Text = "Start walking!",
				FontFamily = "RobotoSlab",
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.Center
			};

		private static ProgressBar CreateProgressBarElement()
			=> new()
			{
				ProgressColor = mainGreen,
				ScaleY = 9,
				Progress = 0
			};
		#endregion

		#region Overriden methods
		protected override void OnControlInitialized(Grid control)
		{
			control.Children.Add(ProgressBar);
			control.Children.Add(ProgressBarText);
		}

		protected override void LayoutChildren(double x, double y, double width, double height)
		{
			base.LayoutChildren(x, y, width, height);
			ProgressBarText.TranslationX = GetXTranslate(ProgressBar.Progress, ProgressBar.Width);
		}
		#endregion
	}
}