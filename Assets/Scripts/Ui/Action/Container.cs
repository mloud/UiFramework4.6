using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Action
{
    public class Container : Core.MonoBehaviourGod
    {
        private List<ActionBase> Actions;

        protected override void Awake()
        {
            // gather all actions on the same component
          Actions = new List<ActionBase>(GetComponents<ActionBase>());
        }

        public T AddAction<T>() where T : ActionBase
        {
            return gameObject.AddComponent<T>();
        }
   }
}
