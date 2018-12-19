using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.pools.example
{
    public class TestBPool : MonoBehaviour
    {
        private void OnTriggerEnter(Collider coll)
        {
            PoolManager.Destroy(gameObject);
        }
    }
}
