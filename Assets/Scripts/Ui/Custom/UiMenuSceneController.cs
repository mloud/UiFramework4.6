using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ui
{
    public class UiMenuSceneController : Ui.Controller
    {
        private UiMenuSceneView View;

        protected override void Awake()
        {
            base.Awake();

            View = GetComponent<UiMenuSceneView>();

            View.IntroButton.onClick.AddListener(() => Core.App.Instance.UiManager.LoadScene(UiDefs.Scene.Intro));
            View.GameButton.onClick.AddListener(() => Core.App.Instance.UiManager.LoadScene(UiDefs.Scene.Game));
        }

    }
}