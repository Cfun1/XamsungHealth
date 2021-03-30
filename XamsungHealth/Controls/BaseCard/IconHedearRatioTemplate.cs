using Xamanimation;
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
			get
			{
				var style = new Style<Frame>(
						(Frame.CornerRadiusProperty, 15),
						(Frame.PaddingProperty, 20),
						(Frame.BackgroundColorProperty, Color.White),
						(Frame.BorderColorProperty, Color.Transparent),
						(TouchEffect.ShouldMakeChildrenInputTransparentProperty, false)
					);

				style.FormsStyle.Triggers.Add(
				new DataTrigger(typeof(Frame))
				{
					Value = true,
					Binding = new Binding(source: RelativeBindingSource.TemplatedParent,
											path: nameof(BaseCard.IsInEditMode)),

					EnterActions =
					{
						new AnimateDouble()
						{
							Duration = 250,
							To = 0.9,
							TargetProperty = Frame.ScaleProperty
						}
					},

					ExitActions =
					{
						new AnimateDouble()
						{
							Duration = 250,
							To = 1,
							TargetProperty = Frame.ScaleProperty
						}
					}
				});

				return style;
			}
		}

		public static Style<CircleIconView> DefaulCircleIconViewStyle
		{
			get => new Style<CircleIconView>(
						(HorizontalOptionsProperty, LayoutOptions.End),
						(VerticalOptionsProperty, LayoutOptions.Start),
						(OpacityProperty, 0),
						(CircleIconView.SizeProperty, 30),
						(CircleIconView.TranslationYProperty, -5)
						//TODO: set this value as a constant because it used in triggers also
						).BasedOn(CircleIconView.DefaultStyle!);
		}

		public Image IconImage { get; set; }
		public IconHedearRatioTemplate()
		{
			var mainFrame = new Frame()
				.Style(DefaulFrameStyle);

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
						Margin = new Thickness(0, 10)
					}
				}
			};

			//Will be used for both + - icons
			var EditModeButton = new CircleIconView()
			{
				Source = new FontImageSource
				{
					Glyph = IconFont.Minus,
					FontFamily = IconFont._FontName,
					Size = 15,
					Color = Color.Red
				},
			}
				.Style(IconHedearRatioTemplate.DefaulCircleIconViewStyle);

			EditModeButton.Triggers.Add(
									new DataTrigger(typeof(CircleIconView))
									{
										Value = true,
										Binding = new Binding(source: RelativeBindingSource.TemplatedParent,
																path: nameof(BaseCard.IsInEditMode)),

										EnterActions =
										{
											new AnimateDouble()
											{
												Duration=250,
												To=0,
												TargetProperty =CircleIconView.TranslationYProperty,
											},
											new AnimateDouble()
											{
												Duration=250,
												To=1,
												TargetProperty =CircleIconView.OpacityProperty,
											},
										},

										ExitActions =
										{
										new AnimateDouble()
											{
												Duration=250,
												To=-5,
												TargetProperty =CircleIconView.TranslationYProperty,
											},
											new AnimateDouble()
											{
												Duration=250,
												To=0,
												TargetProperty =CircleIconView.OpacityProperty,
											},
										}
									}
							);

			mainFrame.Content = mainStackLayout;

			//instead of Grid it could be done with a RelativeLayout also, ConstraintExpression Type=RelativeToView
			Children.Add(mainFrame);
			mainFrame.Bind(TouchEffect.LongPressCommandProperty, source: RelativeBindingSource.TemplatedParent,
					path: nameof(BaseCard.EditModeCommand))
				.Bind(TouchEffect.LongPressCommandParameterProperty, source: RelativeBindingSource.TemplatedParent);
			Children.Add(EditModeButton);
		}
	}
}
