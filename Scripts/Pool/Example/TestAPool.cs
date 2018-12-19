using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.pools.example
{
    public class TestAPool : MonoBehaviour
    {
        public GameObject obj;
        [Range(0, 1)]
        public float time;

        // Use this for initialization
        void Start()
        {
            Create();
        }

        void Create()
        {
            PoolManager.Instantiate(obj, Vector3.down, Quaternion.identity, gameObject.transform);
            Invoke("Create", time);
        }
    }
}

