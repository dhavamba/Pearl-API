using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.actionTrigger
{
    public abstract class ComplexAction : MonoBehaviour
    {
        #region Properties
        public Dictionary<string, object> Informations { get; private set; }
        #endregion

        #region Unity CallBacks
        private void Awake()
        {
            Informations = new Dictionary<string, object>();
            SetAwake();
        }

        private void OnEnable()
        {
            Informations.Clear();
        }
        #endregion

        #region Public Methods
        public void Add<T>(string name, T value)
        {
            Informations.Update(name, value);
        }

        public T Take<T>(string name)
        {
            return (T)Informations[name];
        }
        #endregion

        #region Abstract Methods
        public abstract void SetAction();
        public abstract void SetAwake();
        #endregion
    }
}
