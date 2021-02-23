using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamsungHealth
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			var rngesus = new Random(Guid.NewGuid().GetHashCode());
			var startingAt1 = rngesus.Next(-360, 360);
			var endingAt1 = rngesus.Next(-360, 360);

			Progress1.Animate("Progress1Start", x => Progress1.StartingDegrees = (float)x, Progress1.StartingDegrees, startingAt1, 4, 5000, Easing.CubicInOut);
			Progress1.Animate("Progress1End", x => Progress1.EndingDegrees = (float)x, Progress1.EndingDegrees, endingAt1, 4, 5000, Easing.CubicInOut);
		}
	}
}
