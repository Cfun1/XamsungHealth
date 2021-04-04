using System.Collections.Generic;
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
using System;

namespace XamsungHealth
{
	public class HomeViewModel : ObservableObject//, IBaseDragDrop
	{
		public HomeViewModel()
		{
			AllCardsList = new[]
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
			CardsList = new(AllCardsList.Where(x => x.IsHidden == false).ToList());
			lastVisibleCardIndex = CardsList.Count() - 1;
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
		public bool IsOrderChanged;
		public bool IsHiddenChanged;
		int lastVisibleCardIndex;

		public IEnumerable<BaseCard> AllCardsList { get; }
		List<BaseCard>? changedIsHiddenCardsList;

		List<string>? savedCardsOrder;

		public ObservableCollection<BaseCard> CardsList
		{
			get;
			set;
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
			changedIsHiddenCardsList?.Clear();
			savedCardsOrder?.Clear();
			IsOrderChanged = IsHiddenChanged = IsInEditMode = false;
		}

		internal void Cancel()
		{
			changedIsHiddenCardsList?.Clear();
			savedCardsOrder?.Clear();
			IsOrderChanged = IsHiddenChanged = IsInEditMode = false;
		}

		internal void CheckForChanges()
		{
			IsOrderChanged = CheckForOrderChanged();
			IsHiddenChanged = CheckForIsHiddenChanged();
		}

		bool CheckForIsHiddenChanged()
			=> (changedIsHiddenCardsList is not null && changedIsHiddenCardsList.Count() != 0);

		bool CheckForOrderChanged()
		{
			if (savedCardsOrder is null)
			{
				throw new NullReferenceException(nameof(savedCardsOrder));
			}

			var returnValue = false;
			for (var i = 0; i < CardsList.Count - 1; i++)
			{
				if (!CardsList[i].TitleText.Equals(savedCardsOrder[i]))
				{
					returnValue = true;
					break;
				}
			}
			return returnValue;
		}

		void EditButton(object obj)
		{
			if (obj is not BaseCard baseCard)
			{
				return;
			}

			RelocateOnIsHiddenChanged(baseCard);
			UpdatechangedIsHiddenCardsList(baseCard);
		}

		private void UpdatechangedIsHiddenCardsList(BaseCard baseCard)
		{
			changedIsHiddenCardsList ??= new List<BaseCard>();
			if (changedIsHiddenCardsList.Contains(baseCard))
			{
				changedIsHiddenCardsList.Remove(baseCard);
			}
			else
			{
				changedIsHiddenCardsList.Add(baseCard);
			}
		}

		void RelocateOnIsHiddenChanged(BaseCard baseCard)
		{
			var index = CardsList.IndexOf(baseCard);

			if (baseCard.IsHidden)
			{
				lastVisibleCardIndex++;
				baseCard.IsHidden = false;
				CardsList.Move(index, lastVisibleCardIndex);
			}
			else
			{
				CardsList.Move(index, lastVisibleCardIndex);
				lastVisibleCardIndex--;
				baseCard.IsHidden = true;
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string? propertyName = "")
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName is null)
			{
				return;
			}

			if (propertyName.Equals(nameof(IsInEditMode)))
			{
				UpdateCardsListOnIsInEditModeChanged();

				if (IsInEditMode)   //Entering edit mode
				{
					SaveCurrentCardsOrder();
					CardsList.Move(0, 1);   //todo: delete: fake a reorder
				}
			}
		}

		void SaveCurrentCardsOrder()
		{
			savedCardsOrder ??= new(CardsList.Select(x => x.TitleText));
			if (savedCardsOrder.Count() == 0)
			{
				savedCardsOrder.AddRange(CardsList.Select(x => x.TitleText));
			}
		}

		void UpdateCardsListOnIsInEditModeChanged()
		{
			var list = AllCardsList.Where(x => x.IsHidden == true).ToList();
			foreach (var card in list)
			{
				if (IsInEditMode)
				{
					CardsList.Add(card);
				}
				else
				{
					CardsList.Remove(card);
				}
			}
		}

		internal void RevertChanges()
		{
			if (IsHiddenChanged)
			{
				RestoreIsHidden();
			}

			if (IsOrderChanged)
			{
				RestoreOrder();
			}
		}

		private void RestoreOrder()
		{
			if (savedCardsOrder is null || savedCardsOrder.Count() == 0)
			{
				return;
			}

			for (var i = 0; i < savedCardsOrder.Count() - 1; i++)
			{
				var card = CardsList.Where(x => x.TitleText.Equals(savedCardsOrder[i])).FirstOrDefault();
				var index = CardsList.IndexOf(card);
				if (index != i)
				{
					CardsList.Move(index, i);
				}
			}
		}

		private void RestoreIsHidden()
		{
			if (changedIsHiddenCardsList is null || changedIsHiddenCardsList.Count() == 0)
			{
				return;
			}

			foreach (var card in changedIsHiddenCardsList)
			{
				var cardToRestore = CardsList.Where(x => x.Equals(card)).FirstOrDefault();
				cardToRestore.IsHidden = cardToRestore.IsHidden ? false : true;
			}
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