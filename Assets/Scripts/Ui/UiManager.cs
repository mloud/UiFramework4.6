using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ui 
{
    public class UiManager : Core.MonoBehaviourGod
    {
        public class Environment
        {
            public Ui.Comp.SceneCanvas SceneCanvas;
        }
        
        // Reference to UiFactory
        public IUIFactory Factory { get; private set; }

        public WindowManager WindowManager { get; private set; }

        public Environment Env { get; set;  }

        public Evt.IEventDispatcher EventDispatcher { get; private set; }

        private List<SceneState> StateMachinesBehavs { get; set; }

        public void Init(IUIFactory factory)
        {
            EventDispatcher = new Evt.EventDispatcher();

            // Assign factory
            Factory = factory;

            Env = new Environment();

            // Find canvas
            SetEnvironment();

            // Create WindowManager
            WindowManager = WindowManager.CreateInstance();
            WindowManager.Init(this);
            WindowManager.transform.SetParent(transform);

            Animator = GetComponent<Animator>();

#if UNITY_NEW
            StateMachinesBehavs = new List<SceneState>(Animator.GetBehaviours<SceneState>());
#else
            StateMachinesBehavs = new List<SceneState>();
            var state = new SceneState();
            state.StateName = "Intro";
            state.SceneUi = "UiSceneIntro";
            StateMachinesBehavs.Add(state);

            state = new SceneState();
            state.StateName = "Menu";
            state.SceneUi = "UiSceneMenu";
            StateMachinesBehavs.Add(state);

            state = new SceneState();
            state.StateName = "Game";
            state.SceneUi = "UiSceneGame";
            StateMachinesBehavs.Add(state);


#endif
        }

        private Animator Animator { get; set; }

        private static UiManager instance;


        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
#if UNITY_NEW            
            Animator.SetTrigger(sceneName);
#endif
            var bg = Factory.Create<Ui.TransitionBackground>("TransitionBackground");
            bg.SetParent(Env.SceneCanvas.TopPanel);

            // fade out 
            yield return StartCoroutine(bg.FadeCoroutine(true, 0.1f));

            //leave current scene
            LeaveScene(StateMachinesBehavs.Find(x => x.StateName == Application.loadedLevelName));           

            // load new scene
            Application.LoadLevel(sceneName);

            // enter scene
            EnterScene(StateMachinesBehavs.Find(x => x.StateName == sceneName));

            // fade in
            yield return StartCoroutine(bg.FadeCoroutine(false, 0.1f));
            Destroy(bg.gameObject);
        }

        public void LoadScene(string scene)
        {
            StartCoroutine(LoadSceneCoroutine(scene));
        }

        public static UiManager CreateInstance()
        {
            return Core.App.Instance.Res.Instantiate<UiManager>("Prefabs/Ui/UiManager");
        }

        private void SetEnvironment()
        {
            var sceneCanvas = Factory.Create<Ui.Comp.SceneCanvas>("SceneCanvas");
            DontDestroyOnLoad(sceneCanvas.gameObject);

            Env.SceneCanvas = sceneCanvas;
        }

        private void OnLevelWasLoaded(int level)
        {
            new Evt.Event(Evt.Types.SceneLoaded, level.ToString());
        }


        private void EnterScene(SceneState state)
        {
            Core.Dbg.Log("UiManager.OnSceneStateEnter() " + state.StateName);
            
            if (!string.IsNullOrEmpty(state.SceneUi))
            {
                var sceneUi = Factory.Create<Component>(state.SceneUi);

                sceneUi.SetParent(Env.SceneCanvas.BottomPanel);
            }
        }

        private void LeaveScene(SceneState state)
        {
            Core.Dbg.Log("UiManager.OnSceneStateLeave() " + state.StateName);

            if (!string.IsNullOrEmpty(state.SceneUi))
            {
                Destroy(GameObject.Find(state.SceneUi));
            }
        }
    }
}
