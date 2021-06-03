using Xamarin.Forms;

namespace XamsungHealth.Views
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
			BindingContext = new SettingsViewModel();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
		}
	}
}