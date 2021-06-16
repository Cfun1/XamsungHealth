namespace XamsungHealth.Models
{
	public class MenuItem
	{
		public enum GroupOrder { First, Middle, Last }
		public string Title { get; private set; }
		public string Description { get; private set; }
		public bool HaveSwitch { get; private set; }

		public GroupOrder OrderInGroup;

		public MenuItem(string title, string description, bool haveSwitch, GroupOrder orderInGroup = GroupOrder.Middle)
		{
			Title = title;
			Description = description;
			HaveSwitch = haveSwitch;
			OrderInGroup = orderInGroup;
		}
	}
}
