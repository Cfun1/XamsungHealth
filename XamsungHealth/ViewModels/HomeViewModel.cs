using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using XamsungHealth.Controls;
using XamsungHealth.Lib.Fonts;

namespace XamsungHealth
{
	public class HomeViewModel : BaseViewModel
	{

		static Style<Button> buttonStyle
		{
			get => new Style<Button>(

				(Button.PaddingProperty, 0),
				(Button.VerticalOptionsProperty, LayoutOptions.Center),
				(Button.WidthRequestProperty, 120),
				(Button.HeightRequestProperty, 35),
				(Button.BorderColorProperty, Color.LightGray),
				(Button.BorderWidthProperty, 1),
				(TouchEffect.NativeAnimationProperty, true));
		}


		ObservableCollection<BaseCard>? baseCardsList;
		public ObservableCollection<BaseCard>? BaseCardsList
		{
			get => baseCardsList;
			set => SetProperty(ref baseCardsList, value);
		}

		private bool isInEditMode;

		public bool IsInEditMode
		{
			get => isInEditMode;
			set => SetProperty(ref isInEditMode, value);
		}

		public HomeViewModel()
		{
			BaseCardsList = new()
			{
				new BaseCard()
				{
					TitleText = "Steps",
					TotalNumber = 10000,
					CurrentNumber = 5000,
					PrefixTotal = "steps",
					RigthHeaderItem = new ActivityIndicator()
					{
						IsRunning = true,
						HeightRequest = 15,
						Color = App.MainGreen
					},
					Content = new LabeledProgressBar().Bind(
											LabeledProgressBar.PercentageProperty,
											source: new RelativeBindingSource(RelativeBindingSourceMode.FindAncestor,
											typeof(BaseCard)), path: nameof(BaseCard.Percentage))
				}.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode)),

				new BaseCard()
				{
					TitleText = "Active time",
					TotalNumber = 60,
					CurrentNumber = 1,
					PrefixTotal = "mins",
					Icon = IconFont.Clock,
					RigthRatioViewItem = new Label()
					{
						Text = "425 Kcal  |  0.0Km"
					}
				}.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode)),

				new BaseCard()
				{
					TitleText = "Exercise",
					IsRatioVisible = false,
					Icon = IconFont.Running,
					RigthHeaderItem = new Label()
					{
						Text = "View history"
					},
					Content = new StackLayout()
					{
						Margin = new Thickness(20, 0),
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.Center,
						Spacing = 20,
						Children =
						{
							new CircleIconView()
							{
								Source = new FontImageSource()
								{
									FontFamily = IconFont._FontName,
									Glyph = IconFont.Running,
									Color = Color.Black,
									Size = 20
								},
							},
							new CircleIconView()
							{
								Source = new FontImageSource()
								{
									FontFamily = IconFont._FontName,
									Glyph = IconFont.Walking,
									Color = Color.Black,
									Size = 20
								},
							},
							new CircleIconView()
							{
								Source = new FontImageSource()
								{
									FontFamily = IconFont._FontName,
									Glyph = IconFont.Bicycle,
									Color = Color.Black,
									Size = 20
								},
							},
							new CircleIconView()
							{
								Source = new FontImageSource()
								{
									FontFamily = IconFont._FontName,
									Glyph = IconFont.ListUl,
									Color = Color.Black,
									Size = 20
								},
							}
						}
					}
				}.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode)),

				new BaseCard()
				{
					TitleText = "Food",
					TotalNumber = 950,
					CurrentNumber = 0,
					PrefixTotal = "Kcal",

					RigthHeaderItem = new Button()
					{
						Style = buttonStyle,
						Text = "Add"
					}
				}.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode)),

				new BaseCard()
				{
					TitleText = "Were you asleep",
					IsRatioVisible = false,
					Icon = IconFont.Moon,
					Color = Color.Purple,

					RigthHeaderItem = new Label()
					{
						Text = "OK"
					}
				}.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode))
			};
		}

		private ICommand? saveCommand;
		public ICommand SaveCommand => saveCommand ??= new Command(Save);

		private void Save()
		{
		}

		private ICommand? exitEditModeCommand;
		public ICommand ExitEditModeCommand
			=> exitEditModeCommand ??= new Command(ExitEditMode);

		private void ExitEditMode()
			=> IsInEditMode = false;
	}
}
