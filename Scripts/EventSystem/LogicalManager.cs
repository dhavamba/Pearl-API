using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

namespace it.amalfi.Pearl.events
{
    public abstract class LogicalManager<T> : LogicalSimpleManager where T : struct, IConvertible
    {
        #region Protected Fields
        protected Dictionary<Type, LogicalComponent<T>> listComponents;
        #endregion

        #region Unity CallBacks
        protected override void Awake()
        {
            base.Awake();
            listComponents = new Dictionary<Type, LogicalComponent<T>>();
            CreateComponents();
        }

        protected virtual void OnDestroy()
        {
            if (listComponents != null)
            {
                foreach (LogicalComponent<T> component in listComponents.Values)
                {
                    component.OnDestroy();
                }
            }
            EventsManager.OnAction -= ReceiveEvent;
        }
        #endregion

        #region Public Methods
        public Coroutine Invoke(Action action, float time)
        {
            return StartCoroutine(SubInvoke(action, time));
        }

        public void CancelInvoke(Coroutine coroutine)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }

        public void StopAllInvoke(Coroutine coroutine)
        {
            StopAllCoroutines();
        }
        #endregion

        #region Protected Methods
        protected F GetLogicalComponent<F>() where F : LogicalComponent<T>
        {
            return (F)listComponents[typeof(F)];
        }
        #endregion

        #region Private Methods
        private IEnumerator SubInvoke(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
        #endregion

        #region Abstract Methods
        protected abstract void CreateComponents();
        public abstract void DoComplexAction(T enumerator, params object[] objects);
        #endregion
    }

}