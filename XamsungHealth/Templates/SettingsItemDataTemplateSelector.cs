using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamsungHealth.Views;
using static XamsungHealth.Views.SettingItem;

namespace XamsungHealth
{
	public class SettingsItemDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate? FirstItemDataTemplate { get; set; }
		public DataTemplate? MiddleItemDataTemplate { get; set; }
		public DataTemplate? LastItemDataTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			if (item is not SettingItem settingItem)
			{
				throw new ArgumentNullException(nameof(item), $"shouldn't be null");
			}

			if (FirstItemDataTemplate == null || MiddleItemDataTemplate == null || LastItemDataTemplate == null)
			{
				throw new ArgumentNullException(nameof(FirstItemDataTemplate), $"{nameof(FirstItemDataTemplate)} and {nameof(MiddleItemDataTemplate)}  and {nameof(LastItemDataTemplate)} shouldn't be null");
			}

			return settingItem.OrderInGroup switch
			{
				GroupOrder.First => FirstItemDataTemplate,
				GroupOrder.Middle => MiddleItemDataTemplate,
				GroupOrder.Last => LastItemDataTemplate,
				_ => throw new InvalidEnumArgumentException()
			};
		}
	}
}
