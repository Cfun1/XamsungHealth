using System;
using Xamarin.Forms;
using XamsungHealth.Controls;

namespace XamsungHealth
{
	public class CardsDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate? HiddenCardsDataTemplate { get; set; }
		public DataTemplate? VisibleCardsDataTemplate { get; set; }

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
			return baseCard.IsHidden ? HiddenCardsDataTemplate! : VisibleCardsDataTemplate!;
		}
	}
}
