using System;
using System.Collections.Generic;


namespace Ui.Evt
{
    public interface IEventDispatcher
    {
        void Dispatch(Evt.Event evt);
        void Register(Action<Evt.Event> action);
        void Unregister(Action<Evt.Event> action);
    }
}
