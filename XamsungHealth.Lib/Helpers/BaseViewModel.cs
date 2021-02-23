using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace XamsungHealth
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public INavigation Navigation;

		protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null)
		{
			if (!Equals(field, value))
			{
				field = value;
				OnPropertyChanged(name);
			}
		}

		//USAGE:  you may raise a property changed on another dependent property:
		//if (SetProperty(ref _messageReady, value))  OnPropertyChanged(() => BackgroundColor);
		protected void OnPropertyChanged([CallerMemberName] string name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

	}
}

