using System.Graphics;

namespace XamsungHealth.Lib.Effects
{
	public interface IGraphicsEffect
	{
		void AttachTo(GraphicsView graphicsView);
		void DetachFrom(GraphicsView graphicsView);
		void Draw(ICanvas canvas, RectangleF dirtyRect);
	}
}