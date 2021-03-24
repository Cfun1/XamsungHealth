using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using static Xamarin.CommunityToolkit.Markup.GridRowsColumns;

namespace XamsungHealth.Controls
{
	public class IconHedearRatioTemplate : ContentView
	{
		public Image IconImage { get; set; }
		public IconHedearRatioTemplate()
		{
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
						.Bind(ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.RigthHeaderItem))
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
						.Bind(ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.RatioView))
						.Bind(IsVisibleProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.IsRatioVisible)),
							new ContentView()
							{
								VerticalOptions = LayoutOptions.End,
 							}.Bind(ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(BaseCard.RigthRatioViewItem))
						}
					},

					new ContentPresenter()
					{
						Margin = new Thickness(0,10)
					}
				}
			};
			Content = mainStackLayout;
		}
	}
}
