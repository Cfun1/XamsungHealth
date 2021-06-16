using System.Collections.Generic;
using Xamarin.Essentials;
using XamsungHealth.Models;
using GroupOrder = XamsungHealth.Models.MenuItem.GroupOrder;

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
					new MenuItem("Xamsung account", "...", false, GroupOrder.First),
					new MenuItem("Sync wih Xamsung account", "", true, GroupOrder.Last)
				}),

				new SettingsGroup("General", new()
				{
					new MenuItem("Units of measurment", string.Empty, false, GroupOrder.First),
					new MenuItem("Notifications", string.Empty, false),
					new MenuItem("Marketing notifications", "Get notifications from Xamsung health", true),
					new MenuItem("Accessories", string.Empty, false),
					new MenuItem("Connected services", "Sync Xamsung health data with third-party web accounts.", false),
					new MenuItem("Customisation Service", "Get personalised content based on how you use your phone.", false, GroupOrder.Last)
				}),

				new SettingsGroup("Advanced", new()
				{
					new MenuItem("Show steps on noti. panel", string.Empty, true, GroupOrder.First),
					new MenuItem("Auto detect workouts", string.Empty, true, GroupOrder.Last),
				}),


				new SettingsGroup("Privacy", new()
				{
					new MenuItem("Privacy notice", string.Empty, false, GroupOrder.First),
					new MenuItem("Data permissions", "Allow Xamsung Health functions and htird-party apps to read and write specific data.", false),
					new MenuItem("Phone number", string.Empty, false),
					new MenuItem("Download personal data", "Download all personal data collected by Xamsung related to Xamsung Health.", false),
					new MenuItem("Erase personal data", "Erase all personal data collected by Xamsung related to Xamsung Health.", false, GroupOrder.Last),
				}),

				new SettingsGroup("Information", new()
				{
					new MenuItem("About Xamsung Health", $"Version {VersionTracking.CurrentVersion}", false, GroupOrder.First),
					new MenuItem("Contact us", string.Empty, false, GroupOrder.Last),

				}),
			};
		}
	}

	public class SettingsGroup : List<MenuItem>
	{
		public string Name { get; private set; }
		public SettingsGroup(string name, List<MenuItem> settings) : base(settings)
		{
			Name = name;
		}
	}
}