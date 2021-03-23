using System;
using System.ComponentModel;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.UI.Views.Internals;
using Xamarin.Forms;

namespace XamsungHealth.Controls
{
	[ContentProperty(nameof(Content))]
	public class BaseCard : BaseTemplatedView<IconHedearRatioTemplate>
	{


		public static readonly BindableProperty ColorProperty = BindableProperty.Create(
												propertyName: nameof(Color),
												returnType: typeof(Color),
												declaringType: typeof(BaseCard),
												defaultValue: Color.FromHex("#00CE08"),
												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: ColorChanged);

		private static void ColorChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var iconImage = (bindable as BaseCard)?.Control?.IconImage;
			if (iconImage != null)
			{
				IconTintColorEffect.SetTintColor(iconImage, (Color)newValue);
			}
		}

		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}


		public static readonly BindableProperty IsRatioVisibleProperty = BindableProperty.Create(
												propertyName: nameof(IsRatioVisible),
												returnType: typeof(bool),
												defaultValue: true,
												declaringType: typeof(BaseCard),
												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: IsRatioVisibleChanged);

		private static void IsRatioVisibleChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var baseCard = bindable as BaseCard;
			if (baseCard?.ratioView != null && (bool)newValue == false)
			{
				baseCard.ratioView = null;
			}
		}

		public bool IsRatioVisible
		{
			get { return (bool)GetValue(IsRatioVisibleProperty); }
			set { SetValue(IsRatioVisibleProperty, value); }
		}


		public static readonly BindableProperty ContentProperty =
			BindableProperty.Create(nameof(Content), typeof(View), typeof(BaseCard));

		public View? Content
		{
			get => (View?)GetValue(ContentProperty);
			set => SetValue(ContentProperty, value);
		}

		public static readonly BindableProperty RigthHeaderItemProperty =
			BindableProperty.Create(nameof(RigthHeaderItem), typeof(View), typeof(BaseCard));

		public View? RigthHeaderItem
		{
			get => (View?)GetValue(RigthHeaderItemProperty);
			set => SetValue(RigthHeaderItemProperty, value);
		}

		public static Style<Label> DefaulTitleStyle
		{
			get => new(
						(Label.FontSizeProperty, 15),
						(Label.FontAttributesProperty, FontAttributes.Bold));
		}

		RatioView? ratioView;
		public RatioView? RatioView
		{
			get
			{
				return ratioView ??= new();
			}
		}

		public static readonly BindableProperty IconProperty = BindableProperty.Create(
												propertyName: nameof(Icon),
												returnType: typeof(string),
												declaringType: typeof(BaseCard),
 												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: null);

		public string Icon
		{
			get { return (string)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}


		public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
												propertyName: nameof(TitleText),
												returnType: typeof(string),
												declaringType: typeof(BaseCard),
 												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: null);
		public string TitleText
		{
			get { return (string)GetValue(TitleTextProperty); }
			set { SetValue(TitleTextProperty, value); }
		}


		public static readonly BindableProperty TotalNumberProperty = BindableProperty.Create(
												propertyName: nameof(TotalNumber),
												returnType: typeof(double),
												declaringType: typeof(BaseCard),
												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: OnRatioNumbersChanged);

		public double TotalNumber
		{
			get { return (double)GetValue(TotalNumberProperty); }
			set { SetValue(TotalNumberProperty, value); }
		}


		public static readonly BindableProperty CurrentNumberProperty = BindableProperty.Create(
												propertyName: nameof(CurrentNumber),
												returnType: typeof(double),
												declaringType: typeof(BaseCard),
 												defaultBindingMode: BindingMode.TwoWay,
												propertyChanged: OnRatioNumbersChanged);

		public double CurrentNumber
		{
			get { return (double)GetValue(CurrentNumberProperty); }
			set { SetValue(CurrentNumberProperty, value); }
		}

		private static void OnRatioNumbersChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var baseCard = (bindable as BaseCard);
			if (baseCard != null)
			{
				baseCard.Percentage = (baseCard.CurrentNumber / baseCard.TotalNumber) * 100d;
			}
		}

		public static readonly BindableProperty PrefixTotalProperty = BindableProperty.Create(
												propertyName: nameof(PrefixTotal),
												returnType: typeof(string),
												declaringType: typeof(BaseCard),
 												defaultBindingMode: BindingMode.TwoWay);

		public string PrefixTotal
		{
			get { return (string)GetValue(PrefixTotalProperty); }
			set { SetValue(PrefixTotalProperty, value); }
		}

		public static readonly BindableProperty PercentageProperty = BindableProperty.Create(
												propertyName: nameof(Percentage),
												returnType: typeof(double),
												declaringType: typeof(BaseCard));

		[EditorBrowsable(EditorBrowsableState.Never)]
		public double Percentage
		{
			get { return (double)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, value); }
		}


		protected override void OnControlInitialized(IconHedearRatioTemplate control)
		{

		}
	}
}
