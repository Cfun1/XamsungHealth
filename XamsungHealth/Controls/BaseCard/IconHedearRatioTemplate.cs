using Xamarin.CommunityToolkit.Converters;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using XamsungHealth.Lib.Fonts;
using static Xamarin.CommunityToolkit.Markup.GridRowsColumns;

namespace XamsungHealth.Controls
{
	public class IconHedearRatioTemplate : Grid
	{
		static Style<Frame> DefaulFrameStyle
		{
			get => new(
						(Frame.CornerRadiusProperty, 15),
						(Frame.PaddingProperty, 20),
						(Frame.BackgroundColorProperty, Color.White),
						(Frame.BorderColorProperty, Color.Transparent)
					);
		}

		public static Style<CircleIconView> DefaulCircleIconViewStyle
		{
			get => new Style<CircleIconView>(
						(HorizontalOptionsProperty, LayoutOptions.End),
						(VerticalOptionsProperty, LayoutOptions.Start),
						(CircleIconView.SizeProperty, 25),
						(TranslationXProperty, -15),
						(TranslationYProperty, 10)
					  ).BasedOn(CircleIconView.DefaultStyle!);
		}

		public Image IconImage { get; set; }
		public IconHedearRatioTemplate()
		{
			var mainFrame = new Frame().Style(DefaulFrameStyle)
							.Bind(ScaleProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.IsInEditMode),
							converter: new BoolToObjectConverter()
							{
								TrueObject = .85,
								FalseObject = 1.0
							});

			var title = new Label()
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.Start
			}
							.Bind(Label.TextProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.TitleText))
							.Bind(Label.TextColorProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.Color))
							.Style(BaseCard.DefaulTitleStyle);

			IconImage = new Image()
			{
				VerticalOptions = LayoutOptions.Center,
				Source = new FontImageSource()
				{
					Size = 15,
					FontFamily = "FontAwesome"
				}
					.Bind(FontImageSource.GlyphProperty,
						source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.Icon)),
			};
			IconTintColorEffect.SetTintColor(IconImage, (Color)BaseCard.ColorProperty.DefaultValue);

			var headerGrid = new Grid()
			{
				ColumnDefinitions = Columns.Define(Star, Auto),
				RowDefinitions = Rows.Define(Auto, Star),

				Children = {
					 new StackLayout()
						{
							Orientation = StackOrientation.Horizontal,
							Children =
							{
								IconImage,
								title
							},
						}.Column(0).Row(0),

					new ContentView()
					{
						HorizontalOptions = LayoutOptions.End
					}.Column(1).RowSpan(2)
						.Bind(ContentView.ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.RigthHeaderItem))
				}
			};

			var mainStackLayout = new StackLayout()
			{
				Spacing = 5,
				Children =
				{
					headerGrid,
					//TODO: maybe change to a Grid
					new StackLayout()
					{
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							new ContentView()
							{
								HorizontalOptions = LayoutOptions.StartAndExpand
							}
							.Bind(ContentView.ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.RatioView))
							.Bind(IsVisibleProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.IsRatioVisible)),

							new ContentView()
							{
								VerticalOptions = LayoutOptions.End,
 							}.Bind(ContentView.ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.RigthRatioViewItem))
						}
					},

					new ContentPresenter()
					{
						Margin = new Thickness(0,10)
					}
				}
			};

			var EditModeCloseButton = new CircleIconView()
			{
				Source = new FontImageSource
				{
					Glyph = IconFont.Minus,
					FontFamily = IconFont.FontName,
					Size = 10,
					Color = Color.Red
				}
			}
				.Style(IconHedearRatioTemplate.DefaulCircleIconViewStyle)
				.Bind(IsVisibleProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.IsInEditMode));

			mainFrame.Content = mainStackLayout;
			Children.Add(mainFrame);
			Children.Add(EditModeCloseButton);
		}
	}
}
