using Xamarin.Forms;

namespace XamsungHealth
{
	public partial class App : Application
	{
		public static Color MainGreen
		{
			get => (Color)Current.Resources["MainGreen"];
		}

		public App()
		{
			InitializeComponent();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
