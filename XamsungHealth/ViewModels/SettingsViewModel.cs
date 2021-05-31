using System.Collections.Generic;
using Xamarin.Essentials;
using static XamsungHealth.Views.SettingItem;

namespace XamsungHealth.Views
{
	internal class SettingsViewModel
	{
		public List<SettingsGroup> Settings { get; private set; }

		public SettingsViewModel()
		{
			//TODO: add translation
			Settings = new()
			{
				new SettingsGroup(string.Empty, new()
				{
					new SettingItem("Xamsung account", "...", false, GroupOrder.First),
					new SettingItem("Sync wih Xamsung account", "", true, GroupOrder.Last)
				}),

				new SettingsGroup("General", new()
				{
					new SettingItem("Units of measurment", string.Empty, false, GroupOrder.First),
					new SettingItem("Notifications", string.Empty, false),
					new SettingItem("Marketing notifications", "Get notifications from Xamsung health", true),
					new SettingItem("Accessories", string.Empty, false),
					new SettingItem("Connected services", "Sync Xamsung health data with third-party web accounts.", false),
					new SettingItem("Customisation Service", "Get personalised content based on how you use your phone.", false, GroupOrder.Last)
				}),

				new SettingsGroup("Advanced", new()
				{
					new SettingItem("Show steps on noti. panel", string.Empty, true, GroupOrder.First),
					new SettingItem("Auto detect workouts", string.Empty, true, GroupOrder.Last),
				}),


				new SettingsGroup("Privacy", new()
				{
					new SettingItem("Privacy notice", string.Empty, false, GroupOrder.First),
					new SettingItem("Data permissions", "Allow Xamsung Health functions and htird-party apps to read and write specific data.", false),
					new SettingItem("Phone number", string.Empty, false),
					new SettingItem("Download personal data", "Download all personal data collected by Xamsung related to Xamsung Health.", false),
					new SettingItem("Erase personal data", "Erase all personal data collected by Xamsung related to Xamsung Health.", false, GroupOrder.Last),
				}),

				new SettingsGroup("Information", new()
				{
					new SettingItem("About Xamsung Health", $"Version {VersionTracking.CurrentVersion}", false, GroupOrder.First),
					new SettingItem("Contact us", string.Empty, false, GroupOrder.Last),

				}),
			};
		}
	}

	public class SettingsGroup : List<SettingItem>
	{
		public string Name { get; private set; }
		public SettingsGroup(string name, List<SettingItem> settings) : base(settings)
		{
			Name = name;
		}
	}

	public class SettingItem
	{
		public enum GroupOrder { First, Middle, Last }
		public string Title { get; private set; }
		public string Description { get; private set; }
		public bool HaveSwitch { get; private set; }

		public GroupOrder OrderInGroup;

		public SettingItem(string title, string description, bool haveSwitch, GroupOrder orderInGroup = GroupOrder.Middle)
		{
			Title = title;
			Description = description;
			HaveSwitch = haveSwitch;
			OrderInGroup = orderInGroup;
		}
	}

}