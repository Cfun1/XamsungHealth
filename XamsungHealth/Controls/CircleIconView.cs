using System.Runtime.CompilerServices;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace XamsungHealth.Controls
{
	public class CircleIconView : AvatarView
	{
		//TODO: Add a Tap gesture reconizer with Command
		public static Style<AvatarView> DefaultStyle
		{
			get => new(
			(VerticalOptionsProperty, LayoutOptions.Start),
			(AspectProperty, Aspect.AspectFit),
 			(BorderColorProperty, Color.LightGray),
			(ColorProperty, Color.Transparent));
		}

		public CircleIconView()
		{
			TouchEffect.SetNativeAnimation(this, true);
			TouchEffect.SetNativeAnimationRadius(this, (int)Size / 2);
			Style = DefaultStyle;
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null!)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName.Equals(nameof(Size)))
			{
				TouchEffect.SetNativeAnimationRadius(this, (int)Size / 2);
			}
		}
	}
}
