using System;
using System.Collections.Generic;


namespace Ui.Evt
{
    public class EventDispatcher : IEventDispatcher
    {

        private Action<Evt.Event> EventHandlers;


        public void Dispatch(Evt.Event evt)
        {
            if (EventHandlers != null)
                EventHandlers(evt);
        }

        public void Register(Action<Evt.Event> action)
        {
            EventHandlers += action;        
        }

        public void Unregister(Action<Evt.Event> action)
        {
            if (!Core.App.IsQuitting)
            {
                EventHandlers -= action;
            }
        }
    }
}
