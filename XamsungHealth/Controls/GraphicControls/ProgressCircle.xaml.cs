using System;
using System.ComponentModel;
using System.Graphics;
using Xamarin.Forms;
using XamsungHealth.Lib;
using XamsungHealth.Lib.Extensions;
using Color = System.Graphics.Color;
using XColor = Xamarin.Forms.Color;

namespace XamsungHealth.Controls
{
	//Not done in xaml with shapes and path because of https://github.com/xamarin/Xamarin.Forms/issues/13828
	//Adapted from https://github.com/michaelstonis/SkiaSharpExamples/blob/master/SkiaSharpExamples/Views/CircularProgress.cs
	//Alternative: could be replaced by https://github.com/jsuarezruiz/TemplateUI/blob/master/src/TemplateUI/Controls/CircleProgressBar/CircleProgressBar.cs
	public class ProgressCircle : GraphicsView
	{
		public ProgressCircle()
		{
		}

		#region Bindable Properties

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static BindableProperty StartingAngleProperty =
			BindableProperty.Create(nameof(StartingAngle), typeof(float), typeof(ProgressCircle), 90f);

		public float StartingAngle
		{
			get { return (float)GetValue(StartingAngleProperty); }
			set { SetValue(StartingAngleProperty, value.Clamp(-359.99f, 359.99f)); }
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static BindableProperty EndingAngleProperty =
				BindableProperty.Create(nameof(EndingAngle), typeof(float), typeof(ProgressCircle), 90f);

		public float EndingAngle
		{
			get { return (float)GetValue(EndingAngleProperty); }
			set { SetValue(EndingAngleProperty, value.Clamp(-359.99f, 359.99f)); }
		}

		public static BindableProperty PercentageProperty =
	BindableProperty.Create(nameof(Percentage), typeof(float), typeof(ProgressCircle), 0f,
		propertyChanged: (bindable, oldValue, newValue) => (bindable as ProgressCircle)!.Percentage2Angle((float)newValue));

		public float Percentage
		{
			get { return (float)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, value.Clamp(-359.99f, 359.99f)); }
		}

		public static BindableProperty ProgressThicknessProperty =
			BindableProperty.Create(nameof(ProgressThickness), typeof(float), typeof(ProgressCircle), 12f);

		public float ProgressThickness
		{
			get { return (float)GetValue(ProgressThicknessProperty); }
			set { SetValue(ProgressThicknessProperty, value); }
		}

		public static BindableProperty ProgressColorProperty =
			BindableProperty.Create(nameof(ProgressColor), typeof(XColor), typeof(ProgressCircle), XColor.Default);

		public XColor ProgressColor
		{
			get { return (XColor)GetValue(ProgressColorProperty); }
			set { SetValue(ProgressColorProperty, value); }
		}

		public static BindableProperty MainTextProperty = BindableProperty.Create(nameof(MainText), typeof(string), typeof(ProgressCircle));

		public string MainText
		{
			get { return (string)GetValue(MainTextProperty); }
			set { SetValue(MainTextProperty, value); }
		}

		public static BindableProperty SecondaryTextProperty = BindableProperty.Create(nameof(MainText), typeof(string), typeof(ProgressCircle));

		public string SecondaryText
		{
			get { return (string)GetValue(SecondaryTextProperty); }
			set { SetValue(SecondaryTextProperty, value); }
		}

		public static BindableProperty MainTextFontSizeProperty = BindableProperty.Create(nameof(MainTextFontSize), typeof(double), typeof(ProgressCircle));

		public double MainTextFontSize
		{
			get { return (double)GetValue(MainTextFontSizeProperty); }
			set { SetValue(MainTextFontSizeProperty, value); }
		}

		public static BindableProperty SecondaryTextFontSizeProperty = BindableProperty.Create(nameof(SecondaryTextFontSize), typeof(double), typeof(ProgressCircle));

		public double SecondaryTextFontSize
		{
			get { return (double)GetValue(SecondaryTextFontSizeProperty); }
			set { SetValue(SecondaryTextFontSizeProperty, value); }
		}

		static readonly Color transparent = XColor.Transparent.ToGraphicsColor();
		#endregion

		#region Method
		void Percentage2Angle(float percentage)
		{
			var newValue = (-18f / 5f) * percentage + 90f;
			SetValue(EndingAngleProperty, newValue == -270f ? -269 : newValue);
		}
		#endregion

		#region Overriden Methods
		protected override void OnParentSet()
		{
			base.OnParentSet();

			if (Parent != null)
			{
				InvalidateDraw();
			}
		}
		public override void Draw(ICanvas canvas, RectangleF dirtyRect)
		{
			base.Draw(canvas, dirtyRect);

			canvas.StrokeSize = ProgressThickness;
			canvas.Antialias = true;
			canvas.StrokeLineCap = LineCap.Round;
			canvas.FillColor = transparent;
			canvas.SaveState();

			//BackgroundColor = XColor.Transparent;

			var size = Math.Min(dirtyRect.Width, dirtyRect.Height) * 0.85f - 2 * ProgressThickness;
			var CenterX = dirtyRect.Center.X - size / 2f;
			var CenterY = dirtyRect.Center.Y - size / 2f;

			canvas.StrokeColor = ProgressColor.AddLuminosity(-.3d).ToGraphicsColor();
			canvas.DrawArc(CenterX, CenterY, size, size, 0, 360, false, true);

			canvas.StrokeColor = XColor.Black.ToGraphicsColor();

			var blurrableCanvas = canvas as IBlurrableCanvas;
			blurrableCanvas?.SetBlur(3f);
			canvas.BlendMode = BlendMode.SourceAtop;

			canvas.DrawArc(CenterX, CenterY, size, size, StartingAngle, EndingAngle, true, false);

			blurrableCanvas?.SetBlur(0f);
			canvas.BlendMode = BlendMode.Lighten;       //?? not sure 	//paint.BlendMode = SKBlendMode.SrcOver;
			canvas.StrokeColor = ProgressColor.ToGraphicsColor();

			canvas.DrawArc(CenterX, CenterY, size, size, StartingAngle, EndingAngle, true, false);

			//Draw Text
			canvas.FontColor = XColor.Black.ToGraphicsColor();
			canvas.FontSize = (float)MainTextFontSize;
			canvas.SetToBoldSystemFont();
			canvas.DrawString(MainText, CenterX, CenterY, size, size, HorizontalAlignment.Center, VerticalAlignment.Center);

			canvas.RestoreState();
			canvas.Translate(0, 55);
			canvas.FontSize = (float)SecondaryTextFontSize;
			canvas.FontColor = XColor.Gray.ToGraphicsColor();


			canvas.DrawString(SecondaryText, CenterX, 0, size, size, HorizontalAlignment.Center, VerticalAlignment.Center);


			canvas.RestoreState();
		}
		#endregion
	}
}
