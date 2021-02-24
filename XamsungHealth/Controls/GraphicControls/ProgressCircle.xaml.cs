using System;
using Xamarin.Forms;
using XamsungHealth.Lib;
using XColor = Xamarin.Forms.Color;
using Color = System.Graphics.Color;
using XamsungHealth.Lib.Extensions;
using System.Graphics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamsungHealth.Controls
{
	//Adapted from https://github.com/michaelstonis/SkiaSharpExamples/blob/master/SkiaSharpExamples/Views/CircularProgress.cs
	public class ProgressCircle : GraphicsView, IColor
	{
		public ProgressCircle()
		{

		}

		public static readonly BindableProperty ColorProperty = ColorElement.ColorProperty;

		public XColor Color
		{
			get { return (XColor)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}
		//TODO: to be removed
		public static BindableProperty JaggedProperty =
			BindableProperty.Create(nameof(Jagged), typeof(bool), typeof(ProgressCircle), default(bool));

		public bool Jagged
		{
			get { return (bool)GetValue(JaggedProperty); }
			set { SetValue(JaggedProperty, value); }
		}

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
		propertyChanged: (bindable, oldValue, newValue) => (bindable as ProgressCircle).Percentage2Angle((float)newValue));

		public float Percentage
		{
			get { return (float)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, value.Clamp(-359.99f, 359.99f)); }
		}

		void Percentage2Angle(float percentage)
		{
			var newValue = (-18f / 5f) * percentage + 90f;
			SetValue(EndingAngleProperty, newValue == -270f ? -269 : newValue);
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

		protected override void OnParentSet()
		{
			base.OnParentSet();

			if (Parent != null)
				this.InvalidateDraw();
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
			get { return (string)GetValue(MainTextProperty); }
			set { SetValue(MainTextProperty, value); }
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			switch (propertyName)
			{
				case nameof(MainText) or
					 nameof(SecondaryText) or
					 nameof(StartingAngle) or
					 nameof(EndingAngle) or
					 nameof(ProgressThickness) or
					 nameof(ProgressColor):
					InvalidateDraw();
					break;
			};
		}

		private static readonly Color Transparent = XColor.Transparent.ToGraphicsColor();

		public override void Draw(ICanvas canvas, RectangleF dirtyRect)
		{
			base.Draw(canvas, dirtyRect);
			canvas.SaveState();
			var size = Math.Min(dirtyRect.Width, dirtyRect.Height) - ProgressThickness;

			canvas.StrokeSize = ProgressThickness;
			canvas.Antialias = true;
			canvas.StrokeLineCap = LineCap.Round;
			canvas.FillColor = Transparent;
			//BackgroundColor = XColor.Transparent;

			//if (Jagged)
			//	paint.PathEffect = SKPathEffect.CreateDiscrete(12f, 4f, (uint)Guid.NewGuid().GetHashCode());

			var CenterX = (dirtyRect.Center.X - ProgressThickness + (float)Margin.Right) / 2f;
			var CenterY = dirtyRect.Center.Y / 2f;

			canvas.StrokeColor = ProgressColor.AddLuminosity(-.3d).ToGraphicsColor();
			canvas.DrawArc(CenterX, 0f, size, size, 0, 360, false, true);

			canvas.StrokeColor = XColor.Black.ToGraphicsColor();

			var blurrableCanvas = canvas as IBlurrableCanvas;
			blurrableCanvas?.SetBlur(3f);
			canvas.BlendMode = BlendMode.SourceAtop;

			canvas.DrawArc(CenterX, 0f, size, size, StartingAngle, EndingAngle, true, false);

			blurrableCanvas?.SetBlur(0f);
			canvas.BlendMode = BlendMode.Lighten;       //?? not sure 	//paint.BlendMode = SKBlendMode.SrcOver;
			canvas.StrokeColor = ProgressColor.ToGraphicsColor();

			canvas.DrawArc(CenterX, 0f, size, size, StartingAngle, EndingAngle, true, false);

			//Draw Text
			canvas.FontColor = XColor.Black.ToGraphicsColor();
			canvas.FontSize = 45;
			canvas.SetToBoldSystemFont();
			canvas.DrawString(MainText, CenterX, 0, size, size, HorizontalAlignment.Center, VerticalAlignment.Center);
			canvas.RestoreState();
		}
	}
}
