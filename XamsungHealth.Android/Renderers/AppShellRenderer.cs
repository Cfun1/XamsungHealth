using Android.Content;
using AndroidX.AppCompat.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamsungHealth.Droid.Renderers;

[assembly: ExportRenderer(typeof(XamsungHealth.AppShell), typeof(AppShellRenderer))]
namespace XamsungHealth.Droid.Renderers
{
	public class AppShellRenderer : ShellRenderer
	{
		public AppShellRenderer(Context context) : base(context)
		{

		}

		protected override IShellFlyoutRenderer CreateShellFlyoutRenderer()
		{
			return new CustomShellFlyoutRenderer(this, AndroidContext);
		}

		protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
		{
			return new MyShellToolbarAppearanceTracker(this);
		}
		//try use MessagingCenter to change dynamically
	}


	internal class CustomShellFlyoutRenderer : ShellFlyoutRenderer
	{
		public CustomShellFlyoutRenderer(IShellContext shellContext, Context context) : base(shellContext, context)
		{
		}

		public override void AddDrawerListener(IDrawerListener listener)
		{
			base.AddDrawerListener(listener);
			DrawerSlide += CustomShellFlyoutRenderer_DrawerSlide;
		}

		void CustomShellFlyoutRenderer_DrawerSlide(object sender, DrawerSlideEventArgs e)
		{
			(Shell.Current as AppShell).FlyoutOpenedPercentage = e.SlideOffset;
			(Shell.Current as AppShell).FlyoutWidth = e.DrawerView.Width / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
		}
	}

	class MyShellToolbarAppearanceTracker : ShellToolbarAppearanceTracker
	{
		public MyShellToolbarAppearanceTracker(IShellContext context) : base(context)
		{
		}

		public override void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
		{
			base.SetAppearance(toolbar, toolbarTracker, appearance);
			toolbarTracker.TintColor = Color.Black;
		}
	}
}