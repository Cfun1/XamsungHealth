using System.Runtime.CompilerServices;
using System.Windows.Input;
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


		public static readonly BindableProperty CommandProperty = BindableProperty.Create(
												propertyName: nameof(Command),
												returnType: typeof(ICommand),
												declaringType: typeof(CircleIconView),
 												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: OnCommandChanged);

		private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var circleIconView = (bindable as CircleIconView);
			if (circleIconView is not null && newValue is not null)
			{
				if (circleIconView.GestureRecognizers.Count == 0)
				{
					circleIconView.GestureRecognizers.Add(new TapGestureRecognizer()
					{
						Command = circleIconView.Command
					});
				}
				else
				{
					(circleIconView.GestureRecognizers[0] as TapGestureRecognizer)!.Command = circleIconView.Command;
				}
			}
		}

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
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
