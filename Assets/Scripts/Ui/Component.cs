using System;
using System.Collections;
using UnityEngine;

namespace Ui
{
    public class Component : Core.MonoBehaviourGod
    {
        public string Name { get { return gameObject.name; } }
        
        protected override void Awake()
        {}

        public virtual void Init(object param)
        {}

        public virtual void SetParent(RectTransform parent)
        {
            transform.SetParent(parent);
        }
        
    }

}
