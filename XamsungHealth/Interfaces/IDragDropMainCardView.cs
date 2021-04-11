using XamsungHealth.Controls;

namespace XamsungHealth
{
	internal interface IDragDropMainCardView
	{
		bool IsBeingDragged { get; set; }
		bool IsBeingDraggedOver { get; set; }

		void DragStarting(MainCardView card);
		void DropCompleted(MainCardView card);
		void DragLeave(MainCardView card);
		void DragOver(MainCardView card);
	}
	internal interface IDragDropMainCardViewVm
	{
		void Drop(MainCardView card);
	}
}