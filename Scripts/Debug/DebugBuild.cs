using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace it.amalfi.Pearl.debug
{
    public class DebugBuild : Singleton<DebugBuild>
    {
        private Text text;
        // Use this for initialization
        void Awake()
        {
            text = GetComponentInChildren<Text>();
        }

        public void Log(object obj)
        {
            text.text += obj.ToString() + "\n";
        }
    }
}
