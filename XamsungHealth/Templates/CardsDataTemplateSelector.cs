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

		WeakReference<BaseCard>? baseCardWeakReference;

		BindableObject? Container;
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			if (item is not BaseCard baseCard)
			{
				throw new ArgumentNullException(nameof(item), $"shouldn't be null");
			}

			if (VisibleCardsDataTemplate == null || HiddenCardsDataTemplate == null)
			{
				throw new ArgumentNullException(nameof(HiddenCardsDataTemplate), $"{nameof(HiddenCardsDataTemplate)} and {nameof(HiddenCardsDataTemplate)} shouldn't be null");
			}

			Container = container;

			baseCardWeakReference = new WeakReference<BaseCard>(baseCard);
			baseCard.PropertyChanged += PropertyChangedHandler;

			void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
			{
				if (baseCardWeakReference == null || Container is not CollectionView collectionView)
					return;

				if (e.PropertyName.Equals(nameof(BaseCard.IsHidden)) &&
					baseCardWeakReference.TryGetTarget(out var baseCard))
				{
					collectionView.ItemTemplate = null;
					collectionView.ItemTemplate = this;
				}
			}

			return baseCard.IsHidden ? HiddenCardsDataTemplate! : VisibleCardsDataTemplate!;
		}
	}
}
