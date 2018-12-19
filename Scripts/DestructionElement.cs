using System.Collections;
using UnityEngine;
using System;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl
{
    /// <summary>
    /// This class created a game object that will be destroyed after a specific time.
    /// The Destruction activates an event that represents action.
    /// </summary>
    public class DestructionElement : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 10f)]
        private float time = 0.5f;

        public event EventHandlerSimple<GameObject> OnDestruction;

        #region Unity CallBacks
        private void OnEnable()
        {
            if (time != 0)
                StartCoroutine(DestroyCoroutine(time));
        }

        private void OnDisable()
        {
            Destroy();
        }

        private void OnDestroy()
        {
            Destroy();
        }
        #endregion

        #region Private Methods
        private IEnumerator DestroyCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            GameObjectExtend.Destroy(gameObject);
        }

        private void Destroy()
        {
            StopAllCoroutines();
            OnDestruction?.Invoke(gameObject);
        }
        #endregion
    }
}