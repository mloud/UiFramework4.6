using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Ui
{
	public class DropComponent : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public void OnDrop(PointerEventData data)
		{
			Core.Dbg.Log ("DropComponent.OnDrop() from " + data.pointerDrag.name);

			GameObject draggedGameObject = TakeDraggedGameObject (data);
			
			if (draggedGameObject != null)
			{
				OnCustomDrop(draggedGameObject);
			}
		}
		
		public void OnPointerEnter(PointerEventData data)
		{}
		
		public void OnPointerExit(PointerEventData data)
		{}

		// override this to handle drop action
		protected virtual void OnCustomDrop(GameObject gameObject)
		{
			gameObject.GetComponent<RectTransform> ().SetParent (transform);
		}
		
		private GameObject TakeDraggedGameObject(PointerEventData data)
		{
			var dragComponent = data.pointerDrag.GetComponent<Ui.DragComponent> ();

			GameObject draggedObject = null;

			if (dragComponent != null)
			{
				draggedObject = dragComponent.DraggedObject;
				
				dragComponent.DraggedObject = null;
			}

			return draggedObject;
		}
	}
}