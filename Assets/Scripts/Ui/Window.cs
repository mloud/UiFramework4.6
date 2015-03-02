using System;
using System.Collections;
using UnityEngine;

namespace Ui
{
  
    public class Window : Component
    {
        // Animations  
        public UiDefs.Window.Anims OpenAnim = UiDefs.Window.Anims.None;
        public UiDefs.Window.Anims CloseAnim = UiDefs.Window.Anims.None;

        // Actions
        public Action<Window> OpenStart;
        public Action<Window> OpenFinished;
        public Action<Window> CloseFinished;

        // if true, touches are not sent to other windows lying bellow 
        public bool ConsumeAllTouches;


        private Animator Animator { get; set; }


        protected override void Awake()
        {
            Animator = GetComponent<Animator>();
        }


        public override void Init(object param)
        {
            base.Init(param);
            
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.localScale = Vector3.one;

        }

        public void Open()
        {
            OpenInternal();
        }

     
        private void OpenInternal()
        {
            Core.Dbg.Assert(Animator != null || (Animator == null && OpenAnim == UiDefs.Window.Anims.None), "Window.OpenInternal() open animation set but no animator component found");

            bool playAnimation = Animator != null && OpenAnim != UiDefs.Window.Anims.None;

            if (playAnimation)
            {
                Animator.SetTrigger(OpenAnim.ToString());
            }
            // call actions
            if (OpenStart != null)
                OpenStart(this);
           
            OnOpen();

            // call manually OpenFinished
            if (!playAnimation)
            {
                OnEvent(Ui.UiDefs.Triggers.OpenFinished);
            }
        }


        public void Close()
        {
            Core.Dbg.Assert(Animator != null || (Animator == null && CloseAnim == UiDefs.Window.Anims.None), "Window.Close() close animation set but no animator component found");

            bool playAnimation = Animator != null && CloseAnim != UiDefs.Window.Anims.None;

            if (playAnimation)
            {
                Animator.SetTrigger(CloseAnim.ToString());
            }

            OnClose();

            // call manually CloseFinished
            if (!playAnimation)
                OnEvent(Ui.UiDefs.Triggers.CloseFinished);
        }

     

        public void OnEvent(string eventName)
        {
            if (eventName == Ui.UiDefs.Triggers.CloseFinished)
            {
                if (CloseFinished != null)
                    CloseFinished(this);
            }
            else if (eventName == Ui.UiDefs.Triggers.OpenFinished)
            {
                if (OpenFinished != null)
                    OpenFinished(this);
            }
        }


        protected virtual void OnOpen()
        { }

        protected virtual void OnClose()
        { }

        protected virtual void OnUpdate()
        { }
    }

}
