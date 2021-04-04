using System.ComponentModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.UI.Views.Internals;
using Xamarin.Forms;

namespace XamsungHealth.Controls
{
	//TODO: change bindable properties that won't be used with binding to simple properties
	[ContentProperty(nameof(Content))]
	public class MainCardView : BaseTemplatedView<MainCardViewTemplate>
	{
		public MainCardView()
		{
		}

		public MainCardView(bool isPersistent)
		{
			ControlTemplate = new ControlTemplate(() => { return new MainCardViewTemplate(true); });
		}

		private static void LongPress(object obj)
			=> (obj as MainCardView)?.SetValue(IsInEditModeProperty, true);

		private static void EditModeMainButtonClicked(object obj)
		{
			if (obj is not MainCardView card)
				return;
			card.IsHidden = card.IsHidden ? card.IsHidden = false : card.IsHidden = true;
		}

		RatioView? ratioView;
		public RatioView? RatioView
		{
			get
			{
				return ratioView ??= new();
			}
		}

		#region Bindable properties

		public static readonly BindableProperty IsHiddenProperty = BindableProperty.Create(
												propertyName: nameof(IsHidden),
												returnType: typeof(bool),
												declaringType: typeof(MainCardView),
 												defaultBindingMode: BindingMode.TwoWay);

		public bool IsHidden
		{
			get { return (bool)GetValue(IsHiddenProperty); }
			set { SetValue(IsHiddenProperty, value); }
		}

		public static readonly BindableProperty EditModeMainButtonCommandProperty = BindableProperty.Create(
												propertyName: nameof(EditModeMainButtonCommand),
												returnType: typeof(ICommand),
												declaringType: typeof(MainCardView),
												defaultValue: new Command(EditModeMainButtonClicked));

		public ICommand EditModeMainButtonCommand
		{
			get { return (ICommand)GetValue(EditModeMainButtonCommandProperty); }
			set { SetValue(EditModeMainButtonCommandProperty, value); }
		}

		public static readonly BindableProperty LongPressEditModeCommandProperty = BindableProperty.Create(
														propertyName: nameof(LongPressEditModeCommand),
														returnType: typeof(ICommand),
														declaringType: typeof(MainCardView),
														defaultValue: new Command(LongPress));

		[EditorBrowsable(EditorBrowsableState.Never)]
		public ICommand LongPressEditModeCommand
		{
			get { return (ICommand)GetValue(LongPressEditModeCommandProperty); }
			set { SetValue(LongPressEditModeCommandProperty, value); }
		}

		public static readonly BindableProperty ColorProperty = BindableProperty.Create(
												propertyName: nameof(Color),
												returnType: typeof(Color),
												declaringType: typeof(MainCardView),
												defaultValue: Color.FromHex("#00CE08"),
												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: ColorChanged);

		private static void ColorChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var iconImage = (bindable as MainCardView)?.Control?.IconImage;
			if (iconImage != null)
			{
				IconTintColorEffect.SetTintColor(iconImage, (Color)newValue);
			}
		}

		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}


		public static readonly BindableProperty IsRatioVisibleProperty = BindableProperty.Create(
												propertyName: nameof(IsRatioVisible),
												returnType: typeof(bool),
												defaultValue: true,
												declaringType: typeof(MainCardView),
												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: IsRatioVisibleChanged);

		public bool IsRatioVisible
		{
			get { return (bool)GetValue(IsRatioVisibleProperty); }
			set { SetValue(IsRatioVisibleProperty, value); }
		}

		private static void IsRatioVisibleChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var mainCardView = bindable as MainCardView;
			if (mainCardView?.ratioView != null && (bool)newValue == false)
			{
				mainCardView.ratioView = null;
			}
		}

		public static readonly BindableProperty IsInEditModeProperty = BindableProperty.Create(
										propertyName: nameof(IsInEditMode),
										returnType: typeof(bool),
 										declaringType: typeof(MainCardView),
										defaultBindingMode: BindingMode.TwoWay);

		public bool IsInEditMode
		{
			get { return (bool)GetValue(IsInEditModeProperty); }
			set { SetValue(IsInEditModeProperty, value); }
		}


		public static readonly BindableProperty ContentProperty =
					BindableProperty.Create(nameof(Content), typeof(View), typeof(MainCardView));

		public View? Content
		{
			get => (View?)GetValue(ContentProperty);
			set => SetValue(ContentProperty, value);
		}

		public static readonly BindableProperty RigthHeaderItemProperty =
			BindableProperty.Create(nameof(RigthHeaderItem), typeof(View), typeof(MainCardView));

		public View? RigthHeaderItem
		{
			get => (View?)GetValue(RigthHeaderItemProperty);
			set => SetValue(RigthHeaderItemProperty, value);
		}

		public static readonly BindableProperty RigthRatioViewItemProperty =
		BindableProperty.Create(nameof(RigthRatioViewItem), typeof(View), typeof(MainCardView));

		public View? RigthRatioViewItem
		{
			get => (View?)GetValue(RigthRatioViewItemProperty);
			set => SetValue(RigthRatioViewItemProperty, value);
		}

		public static Style<Label> DefaulTitleStyle
		{
			get => new(
						(Label.FontSizeProperty, 15),
						(Label.FontAttributesProperty, FontAttributes.Bold));
		}

		public static readonly BindableProperty IconProperty = BindableProperty.Create(
														propertyName: nameof(Icon),
														returnType: typeof(string),
														declaringType: typeof(MainCardView),
														 defaultBindingMode: BindingMode.TwoWay,
														propertyChanged: null);

		public string Icon
		{
			get { return (string)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}


		public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
												propertyName: nameof(TitleText),
												returnType: typeof(string),
												declaringType: typeof(MainCardView),
 												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: null);
		public string TitleText
		{
			get { return (string)GetValue(TitleTextProperty); }
			set { SetValue(TitleTextProperty, value); }
		}


		public static readonly BindableProperty TotalNumberProperty = BindableProperty.Create(
												propertyName: nameof(TotalNumber),
												returnType: typeof(float),
												declaringType: typeof(MainCardView),
												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: OnRatioNumbersChanged);

		public float TotalNumber
		{
			get { return (float)GetValue(TotalNumberProperty); }
			set { SetValue(TotalNumberProperty, value); }
		}


		public static readonly BindableProperty CurrentNumberProperty = BindableProperty.Create(
												propertyName: nameof(CurrentNumber),
												returnType: typeof(float),
												declaringType: typeof(MainCardView),
 												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: OnRatioNumbersChanged);

		public float CurrentNumber
		{
			get { return (float)GetValue(CurrentNumberProperty); }
			set { SetValue(CurrentNumberProperty, value); }
		}

		private static void OnRatioNumbersChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var mainCardView = (bindable as MainCardView);
			if (mainCardView != null)
			{
				mainCardView.Percentage = (mainCardView.CurrentNumber / mainCardView.TotalNumber) * 100f;
			}
		}

		public static readonly BindableProperty PrefixTotalProperty = BindableProperty.Create(
												propertyName: nameof(PrefixTotal),
												returnType: typeof(string),
												declaringType: typeof(MainCardView),
 												defaultBindingMode: BindingMode.TwoWay);

		public string PrefixTotal
		{
			get { return (string)GetValue(PrefixTotalProperty); }
			set { SetValue(PrefixTotalProperty, value); }
		}

		public static readonly BindableProperty PercentageProperty = BindableProperty.Create(
												propertyName: nameof(Percentage),
												returnType: typeof(float),
												declaringType: typeof(MainCardView));

		[EditorBrowsable(EditorBrowsableState.Never)]
		public float Percentage
		{
			get { return (float)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, value); }
		}
		#endregion

		protected override void OnControlInitialized(MainCardViewTemplate control)
		{

		}
	}
}
