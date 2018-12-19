using it.twoLives;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.events
{
    /// <summary>
    /// This class manages the communication between different gameobjects 
    /// in a centralized way. For example, the gameobject A must 
    /// receive instructions from the gamebject B: A subscribes to an 
    /// event C of the "EventsManager" class, whereas when B wants to call A, it 
    /// invokes the C event of the "EventsManager" class with the necessary parameters.
    /// </summary>
    public static class EventsManager
    {
        public static event EventHandlerSimple<EventAction, Dictionary<string, object>> OnAction;

        #region Call Event
        public static void CallEventLocal(EventAction action, params Tuple<string, object>[] objects)
        {
            OnAction?.Invoke(action, ReturnDictonary(objects));
        }
        #endregion

        private static Dictionary<string, object> ReturnDictonary(params Tuple<string, object>[] objects)
        {
            Dictionary<string, object> aux = new Dictionary<string, object>();
            foreach (Tuple<string, object> tuple in objects)
            {
                aux.Add(tuple.Item1, tuple.Item2);
            }
            return aux;
        }

    }
}
