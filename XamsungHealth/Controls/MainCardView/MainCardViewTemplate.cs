using System;
using Xamanimation;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using XamsungHealth.Lib.Fonts;
using static Xamarin.CommunityToolkit.Markup.GridRowsColumns;

namespace XamsungHealth.Controls
{
	public class MainCardViewTemplate : Grid
	{
		public Image? IconImage { get; set; }

		readonly CircleIconView? editModeButton;
		public readonly Frame mainFrame = new();

		public static Style<Label> DefaulTitleStyle => new(
			(Label.FontSizeProperty, 15),
			(Label.FontAttributesProperty, FontAttributes.Bold));

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
										path: nameof(MainCardView.IsInEditMode)),

						EnterActions =
						{
							new AnimateDouble()
							{
								Duration = 250,
								To = 0.77,
								TargetProperty = Frame.ScaleProperty,
								Easing = EasingType.SpringIn
							},
						},

						ExitActions =
						{
 							new AnimateDouble()
							{
								Duration = 250,
								To = 1,
								TargetProperty = Frame.ScaleProperty,
								Easing = EasingType.SpringIn
							}
						}
					});

				//TODO: Question Issue: if "Setters" merged in DataTrigger above same binding same value, it won't work, why?
				style.FormsStyle.Triggers.Add(
					new DataTrigger(typeof(Frame))
					{
						Value = true,
						Binding = new Binding(source: RelativeBindingSource.TemplatedParent,
											path: nameof(MainCardView.IsInEditMode)),
						Setters =
						{
							//Question Issue: Why this is working on the last card of the collectionview only ?
							//If reloading xaml with hotreload suddenly starts work on all items

							//new Setter() {
							//	Property = TouchEffect.NativeAnimationProperty,
							//	Value = true
							//},

							new Setter() {
								Property = TouchEffect.ShouldMakeChildrenInputTransparentProperty,
								Value = true
							},
							new Setter() {
								Property = ShadowEffect.ColorProperty,
								Value = Color.Black
							}
 						}
					}
					);

				return style;
			}
		}

		public static Style<CircleIconView> DefaulCircleIconViewStyle
		{
			get => new Style<CircleIconView>(
						(HorizontalOptionsProperty, LayoutOptions.End),
						(VerticalOptionsProperty, LayoutOptions.Start),
						(OpacityProperty, 0),
						(CircleIconView.SizeProperty, 25),
						(CircleIconView.TranslationYProperty, -5),
						(CircleIconView.TranslationXProperty, -10)
						//TODO: set this value as a constant because it used in triggers also
						).BasedOn(CircleIconView.DefaultStyle!);
		}


		public MainCardViewTemplate() : this(false)
		{

		}

		public MainCardViewTemplate(bool isPersistent)
		{
			mainFrame.Style(DefaulFrameStyle);

			var title = new Label()
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.Start
			}
							.Bind(Label.TextProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.TitleText))
							.Bind(Label.TextColorProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.Color))
							.Style(DefaulTitleStyle);

			IconImage = new Image()
			{
				VerticalOptions = LayoutOptions.Center,
				Source = new FontImageSource()
				{
					Size = 15,
					FontFamily = "FontAwesome"
				}
					.Bind(FontImageSource.GlyphProperty,
						source: RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.Icon)),
			};
			IconTintColorEffect.SetTintColor(IconImage, (Color)MainCardView.ColorProperty.DefaultValue);

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
						.Bind(ContentView.ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.RigthHeaderItem))
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
							.Bind(ContentView.ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.RatioView))
							.Bind(IsVisibleProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.IsRatioVisible)),

							new ContentView()
							{
								VerticalOptions = LayoutOptions.End,
							}.Bind(ContentView.ContentProperty, source: RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.RigthRatioViewItem))
						}
					},

					new ContentPresenter()
					{
						Margin = new Thickness(0, 10)
					}
				}
			};

			if (!isPersistent)
			{
				editModeButton = new CircleIconView()
				{
					TranslationX = -35, //TODO: deal with the Magic number :/

					Source = new FontImageSource
					{
						FontFamily = IconFont._FontName,
						Size = 10
					}
					.Bind(FontImageSource.GlyphProperty,
						source: new RelativeBindingSource(
							RelativeBindingSourceMode.FindAncestorBindingContext,
							typeof(MainCardView)),
						path: nameof(MainCardView.IsHidden),
						converter: new BoolToObjectConverter()
						{
							TrueObject = IconFont.Plus,
							FalseObject = IconFont.Minus
						},
						fallbackValue: IconFont.Dizzy //if omitted => java.lang.IllegalArgumentException text cannot be null
					)
					.Bind(FontImageSource.ColorProperty,
						source: new RelativeBindingSource(
								RelativeBindingSourceMode.FindAncestorBindingContext,
								typeof(MainCardView)),
						path: nameof(MainCardView.IsHidden),
						converter: new BoolToObjectConverter()
						{
							TrueObject = Color.Green,
							FalseObject = Color.Red
						})
				}
					.Bind(CircleIconView.CommandParameterProperty,
						source: new RelativeBindingSource(
								RelativeBindingSourceMode.FindAncestorBindingContext,
								typeof(MainCardView))
						)
					.Bind(CircleIconView.CommandProperty,
						source: new RelativeBindingSource(
							RelativeBindingSourceMode.FindAncestorBindingContext,
							typeof(MainCardView)),
						path: nameof(MainCardView.EditModeMainButtonCommand))
					.Style(MainCardViewTemplate.DefaulCircleIconViewStyle);

				editModeButton.Triggers.Add(
										new DataTrigger(typeof(CircleIconView))
										{
											Value = true,
											Binding = new Binding(source: RelativeBindingSource.TemplatedParent,
																	path: nameof(MainCardView.IsInEditMode)),

											//A workaround to set it at the same elevation as the Frame
											Setters =
											{
											new Setter() {
												Property = ShadowEffect.ColorProperty,
												Value = Color.Transparent
											}
											},

											EnterActions =
											{
											new AnimateDouble()
											{
												Duration=150,
												To=5,
												TargetProperty =CircleIconView.TranslationYProperty,
											},
											new AnimateDouble()
											{
												Duration=150,
												To=1,
												TargetProperty =CircleIconView.OpacityProperty,
											},
											},

											ExitActions =
											{
										new AnimateDouble()
											{
												Duration=150,
												To=-5,
												TargetProperty =CircleIconView.TranslationYProperty,
											},
											new AnimateDouble()
											{
												Duration=150,
												To=0,
												TargetProperty =CircleIconView.OpacityProperty,
											},
											}
										}
								);
			}

			mainFrame.Content = mainStackLayout;

			//instead of Grid it could be done with a RelativeLayout also, ConstraintExpression Type=RelativeToView
			Children.Add(mainFrame);
			mainFrame.Bind(TouchEffect.LongPressCommandProperty, source: RelativeBindingSource.TemplatedParent,
					path: nameof(MainCardView.LongPressEditModeCommand))
				.Bind(TouchEffect.LongPressCommandParameterProperty, source: RelativeBindingSource.TemplatedParent);

			if (!isPersistent)
			{
				Children.Add(editModeButton);
			}
			AddDrgDropGestureRconizers();
		}


		void AddDrgDropGestureRconizers()
		{
			var subView = mainFrame;
			var dragGestureRecognizer = new DragGestureRecognizer()
			{
				DragStartingCommand = new Command<MainCardView>((x) =>
				{
					x.IsBeingDragged = true;
					Console.WriteLine(x.TitleText + " is being dragged");
				}),

				DropCompletedCommand = new Command<MainCardView>((x) =>
				{
					x.IsBeingDragged = false;
					Console.WriteLine(x.TitleText + " completed the drop");
				}),
			}
			.Bind(DragGestureRecognizer.DropCompletedCommandParameterProperty, source: RelativeBindingSource.TemplatedParent)
			.Bind(DragGestureRecognizer.DragStartingCommandProperty, source: RelativeBindingSource.TemplatedParent,
										path: nameof(MainCardView.DragStartingCommand))
			.Bind(DragGestureRecognizer.DragStartingCommandParameterProperty, source: RelativeBindingSource.TemplatedParent)

			.Bind(DragGestureRecognizer.DropCompletedCommandProperty, source: RelativeBindingSource.TemplatedParent,
										path: nameof(MainCardView.DropCompletedCommand))
			.Bind(DragGestureRecognizer.DragStartingCommandParameterProperty, source: RelativeBindingSource.TemplatedParent)
			;
			dragGestureRecognizer.SetBinding(DragGestureRecognizer.CanDragProperty,
					new MultiBinding()
					{
						Converter = new VariableMultiValueConverter() { ConditionType = MultiBindingCondition.All },
						Bindings =
						{
							new Binding(nameof(MainCardView.IsHidden), source:RelativeBindingSource.TemplatedParent, converter: new InvertedBoolConverter()),
							new Binding(nameof(MainCardView.IsInEditMode), source:RelativeBindingSource.TemplatedParent),
						}
					});

			var dropGestureRecognizer = new DropGestureRecognizer()
			{
			}
			.Bind(DropGestureRecognizer.DragLeaveCommandProperty, source: RelativeBindingSource.TemplatedParent,
										path: nameof(MainCardView.DragLeaveCommand))
			.Bind(DropGestureRecognizer.DragLeaveCommandParameterProperty, source: RelativeBindingSource.TemplatedParent)
			.Bind(DropGestureRecognizer.DragOverCommandProperty, source: RelativeBindingSource.TemplatedParent,
										path: nameof(MainCardView.DragOverCommand))
			.Bind(DropGestureRecognizer.DragOverCommandParameterProperty, source: RelativeBindingSource.TemplatedParent)
			.Bind(DropGestureRecognizer.DropCommandParameterProperty, source: RelativeBindingSource.TemplatedParent)
			.Bind(DropGestureRecognizer.DropCommandProperty, source: RelativeBindingSource.TemplatedParent,
											path: nameof(MainCardView.DropCommand));

			dropGestureRecognizer.SetBinding(DropGestureRecognizer.AllowDropProperty,
				new MultiBinding()
				{
					Converter = new VariableMultiValueConverter() { ConditionType = MultiBindingCondition.All },
					Bindings =
					{
						new Binding(nameof(MainCardView.IsHidden), source: RelativeBindingSource.TemplatedParent, converter: new InvertedBoolConverter()),
						new Binding(nameof(MainCardView.IsInEditMode), source: RelativeBindingSource.TemplatedParent),
					}
				});

			subView?.GestureRecognizers.Add(dragGestureRecognizer);
			subView?.GestureRecognizers.Add(dropGestureRecognizer);
		}
	}
}