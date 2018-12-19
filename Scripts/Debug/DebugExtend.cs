using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.debug
{
    public static class DebugExtend
    {
        public static void Log(object obj)
        {
            #if UNITY_EDITOR
                Debug.Log(obj);
            return;
            #endif

            #if UNITY_STANDALONE
            #pragma warning disable CS0162 // È stato rilevato codice non raggiungibile
                LogBuild(obj);
            #pragma warning restore CS0162 // È stato rilevato codice non raggiungibile
            #endif
        }

        private static void LogBuild(object obj)
        {
            if (GameObject.FindObjectOfType<DebugBuild>() == null)
            {
                GameObject aux = Resources.Load<GameObject>("DebugBuild");
                aux = GameObject.Instantiate<GameObject>(aux);
                aux.name = "DebugUI";
            }
            DebugBuild.Instance.Log(obj);
        }
    }
}
