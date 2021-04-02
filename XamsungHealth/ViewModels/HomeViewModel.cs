using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XamsungHealth.Controls;
using XamsungHealth.Lib.Fonts;

namespace XamsungHealth
{
	public class HomeViewModel : ObservableObject//, IBaseDragDrop
	{
		public HomeViewModel()
		{
			CardsList = new()
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
				}.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode))
				 .Bind(BaseCard.EditModeMainButtonCommandProperty, source: this, path: nameof(EditButtonCommand)),

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
				}
				.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode))
				.Bind(BaseCard.EditModeMainButtonCommandProperty, source: this, path: nameof(EditButtonCommand)),

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
				}
				.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode))
				.Bind(BaseCard.EditModeMainButtonCommandProperty, source: this, path: nameof(EditButtonCommand)),

				new BaseCard()
				{
					TitleText = "Food",
					TotalNumber = 950,
					CurrentNumber = 0,
					PrefixTotal = "Kcal",

					RigthHeaderItem = new Button()
					{
						Style = ButtonStyle,
						Text = "Add"
					}
				}
				.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode))
				.Bind(BaseCard.EditModeMainButtonCommandProperty, source: this, path: nameof(EditButtonCommand)),

				new BaseCard()
				{
					IsHidden = true,
					TitleText = "Were you asleep (Hidden)",
					IsRatioVisible = false,
					Icon = IconFont.Moon,
					Color = Color.Purple,

					RigthHeaderItem = new Label()
					{
						Text = "OK"
					}
				}
				.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode))
				.Bind(BaseCard.EditModeMainButtonCommandProperty, source: this, path: nameof(EditButtonCommand)),

				new BaseCard()
				{
					IsHidden = true,
					TitleText = "Were you asleep",
					IsRatioVisible = false,
					Icon = IconFont.Moon,
					Color = Color.Purple,

					RigthHeaderItem = new Label()
					{
						Text = "OK"
					}
				}
				.Bind(BaseCard.IsInEditModeProperty, source: this, path: nameof(IsInEditMode))
				.Bind(BaseCard.EditModeMainButtonCommandProperty, source: this, path: nameof(EditButtonCommand))
			};
		}

		static Style<Button> ButtonStyle
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

		#region Properties
		public ObservableCollection<BaseCard>? CardsList { get; set; }
		public ObservableCollection<BaseCard>? VisibleCardsList
		{
			//put both hidden and not hidden in one same list that needs to be initially ordered, push Hidden cards to the end of the list, maybe in future Lazy load on demand (when entering edit mode) them in a separate CollectionView?
			get => new(CardsList.Where(x => x.IsHidden == false).ToList());
			set { }
		}

		bool isInEditMode;
		public bool IsInEditMode
		{
			get => isInEditMode;
			set => SetProperty(ref isInEditMode, value);
		}

		#region Drag/Drop properties
		// bool isBeingDragged;
		//public bool IsBeingDragged
		//{
		//	get { return isBeingDragged; }
		//	set { isBeingDragged = value; }
		//}

		// bool isBeingDraggedOver;
		//public bool IsBeingDraggedOver
		//{
		//	get { return isBeingDraggedOver; }
		//	set { isBeingDraggedOver = value; }
		//}
		#endregion
		#endregion

		#region Commands
		ICommand? saveCommand;
		public ICommand SaveCommand => saveCommand ??= new Command(Save);

		private ICommand? exitEditModeCommand;
		public ICommand ExitEditModeCommand
			=> exitEditModeCommand ??= new Command(ExitEditMode);

		Command<object>? editButtonCommand;
		public Command<object> EditButtonCommand => editButtonCommand ??= new Command<object>(EditButton);


		#region Drag/Drop Commands
		// ICommand? dragStartingCommand;
		//public ICommand DragStartingCommand => dragStartingCommand ??= new Command(DragStarting);

		// ICommand? dropCompletedCommand;
		//public ICommand DropCompletedCommand => dropCompletedCommand ??= new Command(DropCompleted);
		// ICommand? dragOverCommand;
		//public ICommand DragOverCommand => dragOverCommand ??= new Command(DragOver);
		// ICommand? dragLeaveCommand;
		//public ICommand DragLeaveCommand => dragLeaveCommand ??= new Command(DragLeave);
		// ICommand? dropCommand;
		//public ICommand DropCommand => dropCommand ??= new Command(Drop);
		#endregion

		#endregion

		#region Methods
		void Save()
		{
			IsInEditMode = true;
		}

		private void ExitEditMode()
			=> IsInEditMode = false;

		void EditButton(object obj)
		{
			if (obj is not BaseCard baseCard)
				return;
			baseCard.IsHidden = baseCard.IsHidden ? baseCard.IsHidden = false : baseCard.IsHidden = true;
			//here move it down reorder
			//Add it to a temporary list to be ued on SaveCommand or CancelCommand
		}

		// void DragStarting()
		//{
		//}

		// void DropCompleted()
		//{
		//}

		// void DragOver()
		//{
		//}



		// void DragLeave()
		//{
		//}

		// void Drop()
		//{
		//}
		#endregion
	}
}