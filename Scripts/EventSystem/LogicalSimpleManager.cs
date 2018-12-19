using System.Collections.Generic;
using UnityEngine;
using System;

namespace it.amalfi.Pearl.events
{
    public abstract class LogicalSimpleManager : MonoBehaviour
    {
        #region Private Fields
        protected Dictionary<EventAction, Action<Dictionary<string, object>>> receiveEvent;
        #endregion

        #region Unity CallBacks
        protected virtual void Awake()
        {
            EventsManager.OnAction += ReceiveEvent;
            receiveEvent = new Dictionary<EventAction, Action<Dictionary<string, object>>>();
            AddActionWhenReceiveEvent();
            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }
        #endregion

        #region Public Methods
        public void ReceiveEvent(EventAction eventAction, Dictionary<string, object> objects)
        {
            if (receiveEvent.ContainsKey(eventAction))
                receiveEvent[eventAction].Invoke(objects);
        }
        #endregion

        #region Abstract Methods
        protected abstract void AddActionWhenReceiveEvent();
        public abstract void SendOut(EventAction message, params Tuple<string, object>[] objects);
        #endregion
    }
}