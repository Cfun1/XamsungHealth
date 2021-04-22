using System.Runtime.CompilerServices;
using Xamarin.Forms;

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
	}
}