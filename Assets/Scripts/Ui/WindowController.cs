using System;
using System.Collections;
using UnityEngine;

namespace Ui
{
    public class WindowController : Controller
    {
        public Window View { get; set;  }

        protected override void Awake()
        {
            View = GetComponent<Window>();
        }

        public override void Init(object param)
        {}
    }
}
