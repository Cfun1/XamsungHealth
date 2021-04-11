using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamsungHealth.Views.Popups
{
	public partial class CancelPopup : Popup
	{
		public static Size PopupSize
			=> new((DeviceDisplay.MainDisplayInfo.Width /
					DeviceDisplay.MainDisplayInfo.Density),
				0.2 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));
		public CancelPopup()
			=> InitializeComponent();

		public enum ReturnMessages
		{
			Cancel,
			Discard,
			Save
		}

		protected override void LightDismiss()
			=> Dismiss(ReturnMessages.Cancel);

		void Cancel_Clicked(object sender, System.EventArgs e)
		  => Dismiss(ReturnMessages.Cancel);

		void Discard_Clicked(object sender, System.EventArgs e)
		   => Dismiss(ReturnMessages.Discard);

		void Save_Clicked(object sender, System.EventArgs e)
		   => Dismiss(ReturnMessages.Save);
	}
}