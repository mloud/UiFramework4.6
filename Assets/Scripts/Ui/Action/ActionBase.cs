using System;
using System.Collections.Generic;


namespace Ui.Action
{
    [System.Serializable]
    public class ActionBase : Core.MonoBehaviourGod
    {
        // Name of action
        public string Name;
        // trigger event
        public Ui.Evt.Types Type;
        // param of trigger
        public string Param;
        // delay before start
        public float Delay = 0.0f;
        // Action is destroyed after it finishes
        public bool DestroyAfterFinish = true;
        //  Custom OnRun action
        public System.Action OnRunAction;

        public void Set(Ui.Evt.Types type, string param, float delay, bool destroyAfterFinish, System.Action onRunAction)
        {
            Type = type;
            Param = param;
            Delay = delay;
            DestroyAfterFinish = destroyAfterFinish;
            OnRunAction += onRunAction;
        }

        public void Run()
        {
            Invoke("RunInternal", Delay);
        }

        private void RunInternal()
        {
            if (OnRunAction != null)
                OnRunAction();

            OnRun();
        }

        protected virtual void OnRun()
        { }

        protected void OnEnable()
        {
            Evt.Event.Register(OnEventReceived);
        }

        protected void OnDisable()
        {
            Evt.Event.Unregister(OnEventReceived);
        }

        protected virtual bool OnUpdate()
        {
            return false;
        }
       
        protected virtual void OnEventReceived(Evt.Event evt)
        {
			Core.Dbg.Log ("ActionBase.OnEventReceived() " + evt.Type.ToString () + " " + evt.Param);
			//Core.Dbg.Log ("ActionBase.OnEventReceived() is set to " + Type.ToString () + " " + Param);
            if (evt.Type == Type && evt.Param == Param)
            {
               Run();
            }
        }

        private void Update()
        {
            if (OnUpdate() && DestroyAfterFinish)
            {
                Destroy(this);            
            }
        }
    }
}
