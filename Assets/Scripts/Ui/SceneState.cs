using System;
using UnityEngine;

namespace Ui
{
    public class SceneState : State
    {
        public Action<SceneState> EnterAction;
        public Action<SceneState> LeaveAction;


        public string SceneUi;

        public bool ShowLoading;
        public bool SmoothTransition;

        public override void OnEnter()
        {
            Core.Dbg.Log("SceneState.OnEnter()" + StateName);

            if (EnterAction != null)
                EnterAction(this);
        }

        public override void OnLeave()
        {
            Core.Dbg.Log("SceneState.OnLeave()" + StateName);

            if (LeaveAction != null)
                LeaveAction(this);
        }
    }
}
