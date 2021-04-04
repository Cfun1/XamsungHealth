using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamsungHealth.Controls;

namespace XamsungHealth
{
	public class CardsDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate? HiddenCardsDataTemplate { get; set; }
		public DataTemplate? VisibleCardsDataTemplate { get; set; }

		WeakReference<MainCardView>? mainCardViewWeakReference;
		BindableObject? Container;
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			if (item is not MainCardView mainCardView)
			{
				throw new ArgumentNullException(nameof(item), $"shouldn't be null");
			}

			if (VisibleCardsDataTemplate == null || HiddenCardsDataTemplate == null)
			{
				throw new ArgumentNullException(nameof(HiddenCardsDataTemplate), $"{nameof(HiddenCardsDataTemplate)} and {nameof(HiddenCardsDataTemplate)} shouldn't be null");
			}

			Container = container;
			mainCardViewWeakReference = new WeakReference<MainCardView>(mainCardView);
			mainCardView.PropertyChanged += PropertyChangedHandler;

			void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
			{
				if (mainCardViewWeakReference == null || Container is not CollectionView collectionView)
					return;

				if (e.PropertyName.Equals(nameof(MainCardView.IsHidden)) &&
					mainCardViewWeakReference.TryGetTarget(out var mainCardView))
				{
					collectionView.ItemTemplate = null;
					collectionView.ItemTemplate = this;
				}
			}

			return mainCardView.IsHidden ? HiddenCardsDataTemplate! : VisibleCardsDataTemplate!;
		}
	}
}
