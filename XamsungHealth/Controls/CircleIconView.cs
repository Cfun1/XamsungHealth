using System;
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
		public static Style<AvatarView> DefaultStyle => new(
			(VerticalOptionsProperty, LayoutOptions.Start),
			(AspectProperty, Aspect.AspectFit),
 			(BorderColorProperty, Color.LightGray),
			(ColorProperty, Color.Transparent));

		public event EventHandler? Clicked
		{
			add
			{
				if (GestureRecognizers.Count == 0)
				{
					var tapGestureRecognizer = new TapGestureRecognizer();
					tapGestureRecognizer.Tapped += value;
					GestureRecognizers.Add(tapGestureRecognizer);
				}
				else
				{
					(GestureRecognizers[0] as TapGestureRecognizer)!.Tapped += value;
				}

			}

			remove
			{
				if (GestureRecognizers[0] is TapGestureRecognizer tapGestureRecognizer)
				{
					tapGestureRecognizer.Tapped -= value;
				}
			}
		}

		public static readonly BindableProperty CommandProperty = BindableProperty.Create(
												propertyName: nameof(Command),
												returnType: typeof(ICommand),
												declaringType: typeof(CircleIconView),
												 defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: OnCommandChanged);

		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
		  => UpdateCommandAndParameter(bindable, newValue, true);



		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
												propertyName: nameof(CommandParameter),
												returnType: typeof(object),
												declaringType: typeof(CircleIconView),
												 defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: OnCommandParameterChanged);

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
		  => UpdateCommandAndParameter(bindable, newValue, false);

		static void UpdateCommandAndParameter(BindableObject bindable, object newValue, bool isChangedPropertyCommand)
		{
			CircleIconView? circleIconView = bindable as CircleIconView;
			if (circleIconView is not null && newValue is not null)
			{
				if (circleIconView.GestureRecognizers.Count == 0)
				{
					circleIconView.GestureRecognizers.Add(new TapGestureRecognizer()
					{
						Command = circleIconView.Command,
						CommandParameter = circleIconView.CommandParameter  //probably needs to move OnCommandParameterChanged
					});
				}
				else
				{
					if (isChangedPropertyCommand)
					{
						(circleIconView.GestureRecognizers[0] as TapGestureRecognizer)!.Command = circleIconView.Command;
					}
					else
					{
						(circleIconView.GestureRecognizers[0] as TapGestureRecognizer)!.CommandParameter = circleIconView.CommandParameter;
					}
				}
			}
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
