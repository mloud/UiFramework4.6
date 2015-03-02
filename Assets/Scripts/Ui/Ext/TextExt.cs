using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class TextExt : Core.MonoBehaviourGod
    {
        [SerializeField]
        private string textId;

        private Text Text { get; set; }
        private TextMesh TextMesh { get; set; }


        public override void Init()
        {
            Text = GetComponent<Text>();
            TextMesh = GetComponent<TextMesh>();
        }

       
        public void SetText(string text)
        {
            if (Text != null)
            {
                Text.text = text;
            }

            if (TextMesh != null)
            {
                TextMesh.text = text;
            }
        }

    }
}