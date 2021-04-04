using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using XamsungHealth.Views.Popups;

namespace XamsungHealth
{
	public partial class HomePage : ContentPage
	{
		readonly HomeViewModel vm = new();
		public HomePage()
		{
			InitializeComponent();
			BindingContext = vm;
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

		//handeled here because cannot call ShowPopupAsync from VM
		private async void Cancel_Clicked(object sender, System.EventArgs e)
		{
			vm.CheckForChanges();

			if (vm.IsHiddenChanged || vm.IsOrderChanged)
			{
				var popupView = new CancelPopup();
				var popupResult = await Navigation.ShowPopupAsync(popupView!);

				switch (popupResult)
				{
					case CancelPopup.ReturnMessages.Discard:
						vm.RevertChanges();
						break;

					case CancelPopup.ReturnMessages.Save:
						break;

					case CancelPopup.ReturnMessages.Cancel:
						return;
				}
			}
			vm.Cancel();
		}
	}
}
