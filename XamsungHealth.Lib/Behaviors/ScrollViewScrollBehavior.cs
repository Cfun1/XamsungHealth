using System;
using Xamarin.Forms;

namespace XamsungHealth.Lib.Behaviors
{
	//Adapted from https://github.com/jsuarezruiz/Xamanimation/blob/master/Xamanimation/Behaviors/ScrollViewScrollBehavior.cs
	public class CollectionViewScrollBehavior : Behavior<CollectionView>
	{
		public static readonly BindableProperty ScrollXProperty =
			BindableProperty.Create(nameof(ScrollX), typeof(double), typeof(CollectionViewScrollBehavior), default(double),
				BindingMode.TwoWay, null);

		/// <summary>
		/// The horizontal scroll value in pixels.
		/// </summary>
		public double ScrollX
		{
			get { return (double)GetValue(ScrollXProperty); }
			set { SetValue(ScrollXProperty, value); }
		}

		public static readonly BindableProperty ScrollYProperty =
			BindableProperty.Create(nameof(ScrollY), typeof(double), typeof(CollectionViewScrollBehavior), default(double),
				BindingMode.TwoWay, null);

		/// <summary>
		/// The vertical scroll value in pixels.
		/// </summary>
		public double ScrollY
		{
			get { return (double)GetValue(ScrollYProperty); }
			set { SetValue(ScrollYProperty, value); }
		}

		protected override void OnAttachedTo(CollectionView bindable)
		{
			base.OnAttachedTo(bindable);
			bindable.Scrolled += new EventHandler<ItemsViewScrolledEventArgs>(OnScrolled);
		}

		protected override void OnDetachingFrom(CollectionView bindable)
		{
			base.OnDetachingFrom(bindable);
			bindable.Scrolled -= new EventHandler<ItemsViewScrolledEventArgs>(OnScrolled);
		}

		private void OnScrolled(object sender, ItemsViewScrolledEventArgs e)
		{
			ScrollY = e.VerticalOffset;
			ScrollX = e.HorizontalOffset;

			Console.WriteLine(ScrollY);
			//Size contentSize = scrollView.ContentSize;

			//var viewportHeight = contentSize.Height - collectionView.Height;
			//var viewportWidth = contentSize.Width - collectionView.Width;

			//RelativeScrollY = viewportHeight <= 0 ? 0 : e.VerticalOffset / viewportHeight;
			//RelativeScrollX = viewportWidth <= 0 ? 0 : e.HorizontalOffset / viewportWidth;

			//PercentageScrollX = RelativeScrollX * 100;
			//PercentageScrollY = RelativeScrollY * 100;
		}
	}
}