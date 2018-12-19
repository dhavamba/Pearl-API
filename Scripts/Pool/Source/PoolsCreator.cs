using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.pools
{
    /// <summary>
    /// This class creates all the pools in the scene
    /// </summary>
    public class PoolsCreator : MonoBehaviour
    {
        #region Public Fields
        [SerializeField]
        /// <summary>
        /// The prefab of each pool
        /// </summary>
        private GameObject[] prefabs;

        #endregion

        #region Unity CallBacks

        private void Awake()
        {
            foreach (GameObject prefab in prefabs)
            {
                PoolManager.Create(prefab);
            }
        }
        #endregion

    }
}
