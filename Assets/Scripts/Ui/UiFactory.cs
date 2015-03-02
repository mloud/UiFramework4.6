using System;
using UnityEngine;

namespace Ui
{
    public class UiFactory : Core.MonoBehaviourGod, IUIFactory
    {
        [SerializeField]
        string windowsPrefabPath;

        [SerializeField]
        string componentsPrefabPath;

        [SerializeField]
        string sceneUiPrefabPath;

        protected override void Awake()
        {
            base.Awake();

            if (!windowsPrefabPath.EndsWith("/"))
                windowsPrefabPath += "/";

            if (!componentsPrefabPath.EndsWith("/"))
                componentsPrefabPath += "/";

            if (!sceneUiPrefabPath.EndsWith("/"))
                sceneUiPrefabPath += "/";
        }

        public static UiFactory CreateInstance()
        {
            return Core.App.Instance.Res.Instantiate<UiFactory>("Prefabs/Ui/UiFactory");
        }


        public T Create<T>(string uiComponent) where T : MonoBehaviour
        {
            T component = null;

            
            // Window
            if (typeof(T) == typeof(WindowController))
            {
                component = Core.App.Instance.Res.Instantiate<T>(windowsPrefabPath + uiComponent);
            }
            // Window background
            else if (typeof(T) == typeof(WindowBackground))
            {
                component = Core.App.Instance.Res.Instantiate<T>(componentsPrefabPath + uiComponent); 
            }
            else if (typeof(T) == typeof(Ui.TransitionBackground))
            {
                component = Core.App.Instance.Res.Instantiate<T>(componentsPrefabPath + uiComponent); 
            }
            // Scene canvas
            else if (typeof(T) == typeof(Ui.Comp.SceneCanvas))
            {
                component = Core.App.Instance.Res.Instantiate<T>(componentsPrefabPath + uiComponent);
            }
            else if (typeof(T) == typeof(Ui.Component))
            {
                component = Core.App.Instance.Res.Instantiate<T>(sceneUiPrefabPath + uiComponent);
            }

            if (component != null)
                component.gameObject.name = uiComponent;

            return component;
        }

        public void Set(GameObject go)
        {

        }
    }
}
