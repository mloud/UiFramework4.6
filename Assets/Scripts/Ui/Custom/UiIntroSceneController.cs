using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ui
{
    public class UiIntroSceneController : Ui.Controller
    {
        private UiIntroSceneView View;

        protected override void Awake()
        {
            base.Awake();

            View = GetComponent<UiIntroSceneView>();
           
            View.NextButton.onClick.AddListener(() => Core.App.Instance.UiManager.LoadScene(UiDefs.Scene.Menu));
        }

    }
}