using System;
using System.ComponentModel;
using Xamarin.Forms;
using MenuItem = XamsungHealth.Models.MenuItem;
using GroupOrder = XamsungHealth.Models.MenuItem.GroupOrder;

namespace XamsungHealth
{
	public class MenuItemDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate? FirstItemDataTemplate { get; set; }
		public DataTemplate? MiddleItemDataTemplate { get; set; }
		public DataTemplate? LastItemDataTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			if (item is not MenuItem menuItem)
			{
				throw new ArgumentNullException(nameof(item), $"shouldn't be null");
			}

			if (FirstItemDataTemplate == null || MiddleItemDataTemplate == null || LastItemDataTemplate == null)
			{
				throw new ArgumentNullException(nameof(FirstItemDataTemplate), $"{nameof(FirstItemDataTemplate)} and {nameof(MiddleItemDataTemplate)}  and {nameof(LastItemDataTemplate)} shouldn't be null");
			}

			return menuItem.OrderInGroup switch
			{
				GroupOrder.First => FirstItemDataTemplate,
				GroupOrder.Middle => MiddleItemDataTemplate,
				GroupOrder.Last => LastItemDataTemplate,
				_ => throw new InvalidEnumArgumentException()
			};
		}
	}
}
