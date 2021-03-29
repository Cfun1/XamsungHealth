using Xamarin.Forms;

namespace XamsungHealth
{
	public partial class TogetherPage : ContentPage
	{
		public TogetherPage()
		{
			InitializeComponent();
			BindingContext = new HomeViewModel();
		}

	}
}
