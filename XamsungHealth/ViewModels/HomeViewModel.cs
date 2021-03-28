using System.Windows.Input;
using Xamarin.Forms;

namespace XamsungHealth
{
	public class HomeViewModel : BaseViewModel
	{
		private bool isInEditMode;

		public bool IsInEditMode
		{
			get => isInEditMode;
			set => SetProperty(ref isInEditMode, value);
		}

		public HomeViewModel()
		{

		}

		private ICommand? longPressCommand;
		public ICommand LongPressCommand => longPressCommand ??= new Command(LongPress);

		private void LongPress() => IsInEditMode = true;

		private ICommand? saveCommand;
		public ICommand SaveCommand => saveCommand ??= new Command(Save);

		private void Save()
		{
		}

		private ICommand? cancelCommand;
		public ICommand CancelCommand => cancelCommand ??= new Command(Cancel);

		private void Cancel() => IsInEditMode = false;
	}
}
