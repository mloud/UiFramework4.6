using System;
using System.Collections.Generic;


namespace Ui.Action
{
    [System.Serializable]
    public class ActionSound : ActionBase
    {
        public string ClipName;

        protected override void OnRun()
        {
            Core.Dbg.Log("ActionSound.Run()");
        }


        protected override bool OnUpdate()
        {
            return false;
        }
    }
}
