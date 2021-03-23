using Xamarin.Forms;
using GColor = System.Graphics.Color;
using XColor = Xamarin.Forms.Color;

namespace XamsungHealth.Lib.Extensions
{
	public static class ColorExtensions
	{
		public static GColor ToGraphicsColor(this XColor xColor)
		{
			return new GColor((float)xColor.R, (float)xColor.G, (float)xColor.B, (float)xColor.A);
		}

		public static GColor ToGraphicsColor(this XColor xColor, string fallback)
		{
			return xColor != XColor.Default
				? new GColor((float)xColor.R, (float)xColor.G, (float)xColor.B, (float)xColor.A)
				: new GColor(fallback);
		}

		public static GColor ToGraphicsColor(this XColor xColor, XColor fallbackColor)
		{
			return xColor != XColor.Default
				? new GColor((float)xColor.R, (float)xColor.G, (float)xColor.B, (float)xColor.A)
				: new GColor((float)fallbackColor.R, (float)fallbackColor.G, (float)fallbackColor.B, (float)fallbackColor.A);
		}

		public static GColor ToGraphicsColor(this XColor xColor, string lightFallback, string darkFallBack)
		{
			return xColor != XColor.Default
				? new GColor((float)xColor.R, (float)xColor.G, (float)xColor.B, (float)xColor.A)
				: new GColor(Application.Current?.RequestedTheme == OSAppTheme.Light ? lightFallback : darkFallBack);
		}

		public static GColor ToGraphicsColor(this XColor xColor, XColor lightFallbackColor, XColor darkFallBackColor)
		{
			if (xColor != XColor.Default)
			{
				return new GColor((float)xColor.R, (float)xColor.G, (float)xColor.B, (float)xColor.A);
			}
			else
			{
				return Application.Current?.RequestedTheme == OSAppTheme.Light
					? new GColor((float)lightFallbackColor.R, (float)lightFallbackColor.G, (float)lightFallbackColor.B, (float)lightFallbackColor.A)
					: new GColor((float)darkFallBackColor.R, (float)darkFallBackColor.G, (float)darkFallBackColor.B, (float)darkFallBackColor.A);
			}
		}
	}
}