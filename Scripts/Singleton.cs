using UnityEngine;

namespace it.amalfi.Pearl
{
    /// <summary>
    /// The generic singleton pattern
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Private Fields
        private static T instance;
        private static readonly object _lock = new object();
        #endregion

        #region Property
        /// <summary>
        /// The property used to access the static object
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        T[] types = (T[])FindObjectsOfType(typeof(T));
                        if (types.Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it.");
                            return null;
                        }
                        else if (types.Length < 1)
                            return null;
                        else
                            instance = types[0];
                    }
                    return instance;
                }
            }
        }
        #endregion

        #region Unity CallBacks
        protected virtual void OnDestroy()
        {
            instance = null;
        }
        #endregion
    }
}