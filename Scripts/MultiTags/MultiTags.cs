using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace it.amalfi.Pearl.multitags
{
    /// <summary>
    /// Multitags in a gameobject
    /// </summary>
    public class MultiTags : MonoBehaviour
    {
        #region Public Fields
        /// <summary>
        /// List of tags that the gameobject has.
        /// </summary>
        public List<Tags> tags;

        #endregion

        #region Unity CallBacks

        public void Awake()
        {
            if (tags == null)
            {
                tags = new List<Tags>();
            }

            if (tags.Count > 0)
            {
                tags = tags.Distinct<Tags>().ToList<Tags>();
                tags.Sort();
            }
        }

        #endregion
    }
}
