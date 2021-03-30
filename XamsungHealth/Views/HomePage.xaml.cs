using Xamarin.Forms;

namespace XamsungHealth
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
			BindingContext = new HomeViewModel();
		}

		protected override bool OnBackButtonPressed()
		{
			if ((BindingContext as HomeViewModel)!.IsInEditMode == true)
			{
				(BindingContext as HomeViewModel)!.IsInEditMode = false;
				return true;
			}
			return base.OnBackButtonPressed();
		}
	}
}
