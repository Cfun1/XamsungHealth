using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamsungHealth
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}

		void Handle_Tapped(object sender, EventArgs e)
		{
			var rngesus = new Random(Guid.NewGuid().GetHashCode());
			var startingAt1 = rngesus.Next(-360, 360);
			var endingAt1 = rngesus.Next(-360, 360);

			Progress1.Animate("Progress1Start", x => Progress1.StartingAngle = (float)x, Progress1.StartingAngle, startingAt1, 4, 5000, Easing.CubicInOut);
			Progress1.Animate("Progress1End", x => Progress1.EndingAngle = (float)x, Progress1.EndingAngle, endingAt1, 4, 5000, Easing.CubicInOut);
		}
	}
}
