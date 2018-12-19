using UnityEngine;
using UnityEngine.SceneManagement;

namespace it.amalfi.Pearl
{
    /// <summary>
    //  The class makes the unique and indestructible gameobject between the scenes
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour
    {
        #region Unity CallBacks
        private void Awake()
        {
            if (ControlRepeat())
                GameObject.DestroyImmediate(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Private Methods
        /// <summary>
        //  The method check if in the scene there are two gameObject whit the same name
        /// </summary>
        private bool ControlRepeat()
        {
            GameObject aux = gameObject.FindNotMe<DontDestroyOnLoad>();
            return aux != null;
        }
        #endregion
    }
}
