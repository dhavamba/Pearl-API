using it.amalfi.Pearl.events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl
{
    public class Pause : MonoBehaviour
    {
        private bool pause;

        private void Awake()
        {
            EventsManager.OnAction += PauseControl;
            pause = false;
        }

        private void PauseControl(EventAction action, Dictionary<string, object> objects)
        {
            if (action == EventAction.CallPause)
            {
                if (!pause)
                {
                    pause = true;
                    Time.timeScale = 0;
                }
                else
                {
                    pause = false;
                    Time.timeScale = 1;
                }
            }
        }

    }

}