using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class App : MonoBehaviourGod
    {
        public Ui.UiManager UiManager { get; private set; }
        public Ui.IUIFactory UiFactory { get; private set; }
        public Core.IResourceManager Res { get; private set; }

		public static bool IsQuitting;


        public static App Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateInstance();
                }
                return instance;
            }
        }

        private static App instance;
    
        protected override void Awake()
        {
            if (instance == null)
            {

                DontDestroyOnLoad(gameObject);

                instance = this; // for case that App is in scene hierarchy

                Init();
            }
        }


        protected override void Start()
        {
            base.Start();

            UiManager.LoadScene(Ui.UiDefs.Scene.Intro);

            //Invoke("Test", 2.0f);
        }

        public void Test()
        {
            UiManager.WindowManager.OpenWindow("TestWindow");
        }

        public override void Init()
        {
            base.Init();

            // create ResourceManager
            var resInstance = Core.ResourceManager.CreateInstance();
            resInstance.transform.SetParent(transform);
            Res = resInstance;

            // create UiFactory
            var UiFactoryInstance = Ui.UiFactory.CreateInstance();
            UiFactoryInstance.transform.SetParent(transform);
            UiFactory = UiFactoryInstance;

            // create UiManager
            UiManager = Ui.UiManager.CreateInstance();
            UiManager.transform.SetParent(transform);
            UiManager.Init(UiFactory);

        }

        private static App CreateInstance()
        {
            var go = Instantiate(Resources.Load<GameObject>("Prefabs/Core/App")) as GameObject;
            return go.GetComponent<App>();
        }


        private void OnApplicationQuit()
        {
            IsQuitting = true;
        }
    }
}

