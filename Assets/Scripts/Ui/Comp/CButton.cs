using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.Comp
{
    public class CButton : UnityEngine.UI.Button
    {
       
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            new Ui.Evt.Event(Evt.Types.Click, gameObject.name).Send();
        }
    }
}
