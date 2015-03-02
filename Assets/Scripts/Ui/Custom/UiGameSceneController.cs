using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ui
{
    public class UiGameSceneController : Ui.Controller
    {
        private UiGameSceneView View;

        protected override void Awake()
        {
            base.Awake();

            View = GetComponent<UiGameSceneView>();

            View.MenuButton.onClick.AddListener(() => Core.App.Instance.UiManager.LoadScene(UiDefs.Scene.Menu));
        }

    }
}