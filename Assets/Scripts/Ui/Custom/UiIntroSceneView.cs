using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ui
{
    public class UiIntroSceneView : Ui.Component
    {
        public Button NextButton;


        public override void SetParent(RectTransform parent)
        {
            base.SetParent(parent);

            transform.localPosition = Vector3.zero;
            (transform as RectTransform).offsetMin = Vector2.zero;
            (transform as RectTransform).offsetMax = Vector2.one;
        }
    }
}