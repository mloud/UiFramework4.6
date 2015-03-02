using System;
using System.Collections.Generic;
using UnityEngine;


namespace Ui.Action
{
    [System.Serializable]
    public class ActionEnabler : ActionBase
    {
        public bool Enable;
        public List<GameObject> GameObjects = new List<GameObject>();


        protected override void OnRun()
        {
            foreach (var go in GameObjects)
                go.SetActive(Enable);

            // Send event that action finished
            new Evt.Event(Evt.Types.ActionFinished, Name).Send();
        }

        protected override bool OnUpdate()
        {
            return false;
        }

    }
}
