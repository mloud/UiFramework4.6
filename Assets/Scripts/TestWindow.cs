using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Ui
{
    public class TestWindow : Window
    {
        [SerializeField]
        Button btn;

        [SerializeField]
        Button btn1;
        
        [SerializeField]
        Button btn2;
       
        [SerializeField]
        Button btn3;

        public override void Init(object param)
        {
            base.Init(param);

            //var acontainer = btn.gameObject.AddComponent<Ui.Action.Container>();

            //var action = acontainer.AddAction<Ui.Action.ActionEnabler>();
            //action.Set(Evt.Types.WindowOpenFinished, Name, 2, true,  () =>
            //{
            //   Core.Dbg.Log("Test", Core.Dbg.MessageType.Info);
            //});
            //action.Enable = false;
            //action.GameObjects.Add(btn.gameObject);

            //btn1.onClick.AddListener(() => AllocateTexture(1024, 1024));

        }

        private void AllocateTexture(int w, int h)
        {
            Texture2D tex = new Texture2D(w, h);

            for (int i = 0; i < w * h; ++i)
            {
                tex.SetPixel(i / w, i * w, Color.blue);
            }

            GameObject.Find("GameObject").GetComponent<SpriteRenderer>().material.mainTexture = tex;
            tex.Apply();

     
        }

    }

}