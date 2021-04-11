using System;
using System.Collections.Generic;
using System.Graphics;
using XamsungHealth.Lib.Effects;
using Point = System.Graphics.Point;

namespace XamsungHealth.Lib
{
	public interface IGraphicsEffects
	{
		IList<IGraphicsEffect> GraphicsEffects { get; }
	}

	public interface IGraphicsView : IDrawable
	{
		event EventHandler Loaded;
		event EventHandler Unloaded;

		event EventHandler TouchDown;
		event EventHandler TouchMove;
		event EventHandler TouchUp;

		void Load();
		void Unload();

		void OnTouchDown(Point point);
		void OnTouchMove(Point point);
		void OnTouchUp(Point point);
	}
}