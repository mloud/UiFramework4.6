using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Ui
{
	public class DragComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public enum DraggedCreateTime
		{
			CreateOnBegin, // Dragged object is created at the start
			CreateOnDrop   // Dragged object is cerated at drop 
		}
		
		[SerializeField]
		DraggedCreateTime DraggedObjectCreateTime;
		
		[SerializeField]
		public bool CreateCopy;  // If true, Dragged object is a copy this gameobject, otherwise virtual CreateDraggingObject() method is called


		[SerializeField]
		public bool UseDirectionRestriction; // if true drag is started only if angle between drag direction and directionRestriction is less than restrictionAngle

		[SerializeField]
		public Vector2 DirectionRestriction;

		[SerializeField]
		public float RestrictionAngle;


		public GameObject DraggedObject { get; set; }
		
		private RectTransform DraggingPlane { get; set; }

		private ScrollRect ParentScrollRect { get; set; }

		private bool CanDrag { get; set; }

		public void Start()
		{
			// check if DragComponent has ScrollRect as a parent if so we need to send OnBeginDrag, OnDrag and OnEndDrag manually to this ScrollRect otherwise it's
			// consumed here only. This is just a temporary solution!
			//ParentScrollRect = Utils.Go.FindComponentInParent<ScrollRect> (gameObject);
		}

		public virtual GameObject CreateCustomDraggingObject()
		{
			return CreateGameObjectCopy ("DraggedObject");
		}
		
		public virtual GameObject CreateGameObjectCopy(string name)
		{
            var go = Instantiate(this.gameObject) as GameObject;
            go.name = name;
            return go;
		}
		
		public void OnBeginDrag(PointerEventData eventData)
		{
			Core.Dbg.Log ("DragComponent.OnBeginDrag() from " + gameObject.name);

			CanDrag = true;

			if (UseDirectionRestriction)
			{
				float angle = Mathf.DeltaAngle(Mathf.Atan2(DirectionRestriction.y, DirectionRestriction.x) * Mathf.Rad2Deg,
				                               Mathf.Atan2(eventData.delta.y, eventData.delta.x) * Mathf.Rad2Deg);

				if (Mathf.Abs(angle) > RestrictionAngle)
				{
					CanDrag = false;
				}
			}

			if (CanDrag)
			{
                DraggedObject = CreateCopy ? CreateGameObjectCopy(gameObject.name) : CreateCustomDraggingObject();
               
				Canvas canvas = GetMainCanvas ();
		
				DraggingPlane = canvas.transform as RectTransform; // Drag on this plane

				if (DraggedObject != null)
				{
						DraggedObject.transform.SetParent (canvas.transform, false);
						DraggedObject.transform.SetAsLastSibling ();  // in front
						DraggedObject.AddComponent<IgnoreRaycast> (); // ignore raycast under cursor
			
						SetDraggedPosition (eventData);
				}
			}
			else
			{
				if (ParentScrollRect != null)
				{
					ParentScrollRect.OnBeginDrag(eventData);
				}
			}
		}
		
		public void OnDrag(PointerEventData eventData)
		{
			if (CanDrag)
			{
				if (DraggedObject != null)
				{
					SetDraggedPosition(eventData);
				}
			}
			else
			{
				if (ParentScrollRect != null)
				{
					ParentScrollRect.OnDrag(eventData);
				}
			}
		}
		
		
		public void OnEndDrag(PointerEventData eventData)
		{
			Core.Dbg.Log ("DragComponent.OnEndDrag() from " + gameObject.name);

			if (CanDrag)
			{
				if (DraggedObject != null)
				{
					Destroy(DraggedObject);
				}
			}
			else
			{
				if (ParentScrollRect != null)
				{
					ParentScrollRect.OnEndDrag(eventData);
				}
			}
		}
		
		private void SetDraggedPosition(PointerEventData data)
		{
			var rt = DraggedObject.GetComponent<RectTransform>();
			
			Vector3 globalMousePos;
			
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
			{
				rt.position = globalMousePos;
				rt.rotation = DraggingPlane.rotation;
			}
		}

		private Canvas GetMainCanvas()
		{
            return Core.App.Instance.UiManager.Env.SceneCanvas.Canvas;
		}
	}
}
