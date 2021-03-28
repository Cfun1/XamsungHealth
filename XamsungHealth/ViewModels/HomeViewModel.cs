using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using XamsungHealth.Controls;
using XamsungHealth.Lib.Fonts;

namespace XamsungHealth
{
	public class HomeViewModel : BaseViewModel
	{

		//ObservableCollection<BaseCard>? baseCardsList;
		//public ObservableCollection<BaseCard>? BaseCardsList
		//{
		//	get => baseCardsList;
		//	set => SetProperty(ref baseCardsList, value);
		//}

		private bool isInEditMode;

		public bool IsInEditMode
		{
			get => isInEditMode;
			set => SetProperty(ref isInEditMode, value);
		}

		public HomeViewModel()
		{
			//	BaseCardsList = new()
			//	{
			//		new BaseCard()
			//		{
			//			TitleText = "Steps",
			//			TotalNumber = 10000,
			//			CurrentNumber = 5000,
			//			PrefixTotal = "steps",
			//			RigthHeaderItem = new ActivityIndicator()
			//			{
			//				IsRunning = true,
			//				HeightRequest = 15,
			//				Color = App.MainGreen
			//			},
			//			Content = new LabeledProgressBar().Bind(
			//									LabeledProgressBar.PercentageProperty,
			//									source: new RelativeBindingSource(RelativeBindingSourceMode.FindAncestor,
			//									typeof(BaseCard)), path: nameof(BaseCard.Percentage))
			//		}.Bind(BaseCard.IsInEditModeProperty, nameof(IsInEditMode)),

			//		new BaseCard()
			//		{
			//			TitleText = "Active time",
			//			TotalNumber = 60,
			//			CurrentNumber = 1,
			//			PrefixTotal = "mins",
			//			Icon = IconFont.Clock,
			//			RigthRatioViewItem = new Label()
			//			{
			//				Text = "425 Kcal  |  0.0Km"
			//			}
			//		}.Bind(BaseCard.IsInEditModeProperty, nameof(IsInEditMode)),

			//		new BaseCard()
			//		{
			//			TitleText = "Exercise",
			//			IsRatioVisible = false,
			//			Icon = IconFont.Running,
			//			RigthHeaderItem = new Label()
			//			{
			//				Text = "View history"
			//			}

			//		}.Bind(BaseCard.IsInEditModeProperty, nameof(IsInEditMode)),
			//		new BaseCard()
			//		{
			//			TitleText = "Food",
			//			TotalNumber = 950,
			//			CurrentNumber = 0,
			//			PrefixTotal = "Kcal",
			//			RigthHeaderItem = new Button()
			//			{
			//				Text = "Add"
			//			}
			//		}.Bind(BaseCard.IsInEditModeProperty, nameof(IsInEditMode)),
			//	};
		}

		private ICommand? longPressCommand;
		public ICommand LongPressCommand => longPressCommand ??= new Command(LongPress);

		private void LongPress() => IsInEditMode = true;

		private ICommand? saveCommand;
		public ICommand SaveCommand => saveCommand ??= new Command(Save);

		private void Save()
		{
		}

		private ICommand? exitEditModeCommand;
		public ICommand ExitEditModeCommand => exitEditModeCommand ??= new Command(ExitEditMode);

		private void ExitEditMode() => IsInEditMode = false;
	}
}
