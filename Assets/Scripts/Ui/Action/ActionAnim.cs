using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Action
{
    [System.Serializable]
    public class ActionAnim : ActionBase
    {
        public string ClipName;

        public Animation Animation;

        private bool Started { get; set; }

        protected override void OnRun()
        {
            Animation.gameObject.SetActive(true);
            Animation.Play();

            Started = true;
        }

        protected override bool OnUpdate()
        {
            if (Started && !Animation.isPlaying)
            {
                new Evt.Event(Evt.Types.ActionFinished, Name).Send();

                Started = false;

                return true;
            }
            return false;
        }
    }
}
