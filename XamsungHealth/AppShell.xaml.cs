using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamsungHealth.Views;

namespace XamsungHealth
{
	public partial class AppShell : Shell
	{
		float flyoutOpenedPercentage;
		public float FlyoutOpenedPercentage
		{
			get => flyoutOpenedPercentage;
			set
			{
				flyoutOpenedPercentage = value;
				OnPropertyChanged(nameof(FlyoutOpenedPercentage));
			}
		}

		public AppShell()
		{
			InitializeComponent();
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null!)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName.Equals(nameof(FlyoutIsPresented)) && !FlyoutIsPresented)
			{
				FlyoutOpenedPercentage = 0f;
			}

			if (propertyName.Equals(nameof(FlyoutOpenedPercentage)))
			{
				if (Current.CurrentPage != null)
				{
					Current.CurrentPage.TranslationX = Current.FlyoutWidth * FlyoutOpenedPercentage;

					if (Current.CurrentPage.GetValue(TitleViewProperty) is ContentView titleView)
					{
						titleView.TranslationX = Current.FlyoutWidth * FlyoutOpenedPercentage;
					}
				}
			}
		}
		async Task HideFlyoutAndpushAsync(ContentPage page)
		{
			Current.FlyoutIsPresented = false;
			Current.FlyoutBehavior = FlyoutBehavior.Disabled;
			await Current.Navigation.PushAsync(page);
		}

		async void SettingsButton_Clicked(object sender, System.EventArgs e)
			=> await HideFlyoutAndpushAsync(new SettingsPage());

		async void Profile_Clicked(object sender, System.EventArgs e)
			=> await HideFlyoutAndpushAsync(new ProfilePage());
	}
}