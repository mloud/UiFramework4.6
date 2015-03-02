using System;
using System.Collections.Generic;
using UnityEngine;


namespace Ui
{
    public class WindowManager : Core.MonoBehaviourGod
    {
        // Currently opened windows
        private List<WindowController> Windows { get; set; }

        // Windows being closed
        private List<WindowController> ClosingWindows { get; set; }
        
        // Windows waiting to be opened
        private List<WindowController> WindowsToOpen { get; set; }

        // Reference to UIManager
        private UiManager UiManager { get; set; }

        // Creates instance
        public static WindowManager CreateInstance()
        {
            var go = (Instantiate(Resources.Load<GameObject>("Prefabs/Ui/WindowManager")) as GameObject);
            return go.GetComponent<WindowManager>();
        }

        public void Init(UiManager uiManager)
        {
            base.Init();

            Evt.Event.Register(OnEventReceived);

            UiManager = uiManager;

            Windows = new List<WindowController>();
            ClosingWindows = new List<WindowController>();
            WindowsToOpen = new List<WindowController>();
        }
     
        // Open Window by name and param
        public WindowController OpenWindow(string name, object param = null)
        {
            // Use factory to create window object
            var windowCtrl = UiManager.Factory.Create<WindowController>(name);

            // Place window under canvas hierarchy
            windowCtrl.View.gameObject.transform.SetParent(UiManager.Env.SceneCanvas.transform);

            // register actions here
            windowCtrl.View.OpenFinished += this.OnWindowOpen;
            windowCtrl.View.CloseFinished += this.OnWindowClosed;

            windowCtrl.View.Init(param);


            // add to list of windows
            Windows.Add(windowCtrl);

            new Evt.Event(Evt.Types.WindowOpen, name).Send();

            // if any window is being closed, wait for finish
            if (ClosingWindows.Count > 0)
            {
                WindowsToOpen.Add(windowCtrl);
            }
            // open now
            else
            {
                windowCtrl.View.Open();
            }

            return windowCtrl;
        }

        // Close Window by name
        public void CloseWindow(string name)
        {
            if (IsOpen(name))
            {
                int index = Windows.FindIndex(x => x.View.Name == name);

                var win = Windows[index];

                Windows.RemoveAt(index);

                win.View.Close();

                ClosingWindows.Add(win);

                new Evt.Event(Evt.Types.WindowClose, name).Send();
            }
        }

        
        public bool IsOpen(string name)
        {
            return Windows.Find(x => x.View.Name == name) != null;
        }

        private void OnWindowClosed(Window window)
        {
            ClosingWindows.RemoveAt(ClosingWindows.FindIndex(x=>x.View == window));

            new Evt.Event(Evt.Types.WindowCloseFinisihed, name).Send();

            Destroy(window.gameObject);

            if (WindowsToOpen.Count > 0)
            {
                WindowsToOpen[0].View.Open();
                WindowsToOpen.RemoveAt(0);
            }
        }

        private void OnWindowOpen(Window window)
        {
            new Evt.Event(Evt.Types.WindowOpenFinished, window.name).Send();

            // window opening finished - check for inactive background
            if (window.ConsumeAllTouches)
            {
                var bg = UiManager.Factory.Create<WindowBackground>("WindowBackground");
                bg.SetParent(window.transform as RectTransform);
            }

            if (WindowsToOpen.Count > 0)
            {
                WindowsToOpen[0].View.Open();
                WindowsToOpen.RemoveAt(0);
            }
        }


        private void OnDestroy()
        {
            Evt.Event.Unregister(OnEventReceived);
        }


        // events
        private void OnEventReceived(Evt.Event evt)
        {
            if (evt.Type == Evt.Types.SceneLoaded)
            {
                Windows.Clear();
                ClosingWindows.Clear();
                WindowsToOpen.Clear();
            }
        }
    }
}
