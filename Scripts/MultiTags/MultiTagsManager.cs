using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl;

namespace it.amalfi.Pearl.multitags
{
    /// <summary>
    /// Auxiliary methods that allow you to take advantage of the multitags
    /// </summary>
    public static class MultiTagsManager
    {
        /// <summary>
        /// Search gameobjects with tags.
        /// </summary>
        /// <param name = "only">This bool specifies whether the Gameobject should have only those tags</param>
        /// <param name = "tagsParameter">The tags that must have the object</param>
        public static GameObject[] FindGameObjectsWithMultiTags(bool only, params Tags[] tagsParameter)
        {
            Debug.Assert(tagsParameter != null && tagsParameter.Length != 0);

            List<GameObject> listGameObjectsFound = new List<GameObject>();
            MultiTags[] MultiTagsList = GameObject.FindObjectsOfType(typeof(MultiTags)) as MultiTags[];

            foreach (MultiTags multiTags in MultiTagsList)
            {
                if ((!only && multiTags.tags.Count < tagsParameter.Length) || (only && multiTags.tags.Count != tagsParameter.Length))
                    continue;

                if (AreThereTagsInList(multiTags.tags, tagsParameter))
                    listGameObjectsFound.Add(multiTags.gameObject);
            }

            if (listGameObjectsFound.Count > 0)
                return listGameObjectsFound.ToArray();
            else
                return null;
        }

        /// <summary>
        /// Search gameobjects with these tags.
        /// </summary>
        /// <param name = "only">This bool specifies whether the Gameobject should have only those tags</param>
        /// <param name = "tagsParameter">The tags that must have the object</param>
        public static GameObject FindGameObjectWithMultiTags(bool only, params Tags[] tagsParameter)
        {
            Debug.Assert(tagsParameter != null && tagsParameter.Length != 0);

            GameObject[] objects = FindGameObjectsWithMultiTags(only, tagsParameter);
            if (objects != null)
                return objects[0];
            else
                return null;
        }

        #region Extend Methods
        /// <summary>
        /// Are there tags in this GameObject?
        /// </summary>
        /// <param name = "value">The gameobject</param>
        /// <param name = "tagsParameter">The tags that must have the GameObject</param>
        public static bool HaveTags(this GameObject value, params Tags[] tagsParameter)
        {
            Debug.Assert(value != null && tagsParameter != null);

            MultiTags CurrentMultiTagsComponent = value.GetComponent<MultiTags>();
            if (CurrentMultiTagsComponent == null)
                return !(tagsParameter.Length > 0);
            return AreThereTagsInList(CurrentMultiTagsComponent.tags, tagsParameter);
        }

        /// <summary>
        /// Return all tags in the specific GameObject
        /// </summary>
        /// <param name = "value">The specific gameobject</param> 
        public static List<Tags> ReturnTags(this GameObject value)
        {
            Debug.Assert(value != null);

            MultiTags CurrentMultiTagsComponent = value.GetComponent<MultiTags>();
            if (CurrentMultiTagsComponent == null || CurrentMultiTagsComponent.tags.Count < 0)
                return null;
            return CurrentMultiTagsComponent.tags;
        }

        /// <summary>
        /// Add these tags in the specific GameObject
        /// </summary>
        /// <param name = "value">The specific gameobject</param>
        /// <param name = "tagsParameter">The tags that must be added to the GameObject</param>
        public static void AddTags(this GameObject value, params Tags[] tagsParameter)
        {
            Debug.Assert(value != null && tagsParameter != null);

            MultiTags CurrentGameComponent = value.AddOnlyOneComponent<MultiTags>();
            foreach (Tags tag in tagsParameter)
            {
                if (CurrentGameComponent.tags.BinarySearch(tag) < 0)
                    CurrentGameComponent.tags.Add(tag);
            }
            CurrentGameComponent.tags.Sort();
        }

        /// <summary>
        /// Remove these tags in the specific GameObject
        /// </summary>
        /// <param name = "value">The specific gameobject</param>
        /// <param name = "tagsParameter">The tags that must be removed to the GameObject</param>
        public static void RemoveTags(this GameObject value, params Tags[] tagsParameter)
        {
            Debug.Assert(value != null && tagsParameter != null);

            MultiTags CurrentGameComponent = value.GetComponent<MultiTags>();
            if (CurrentGameComponent == null || tagsParameter.Length == 0)
                return;

            foreach (Tags tag in tagsParameter)
                CurrentGameComponent.tags.Remove(tag);

            CurrentGameComponent.tags.Sort();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Is method checks if the tags in the parameters all exist in a list of strings
        /// </summary>
        /// <param name = "tags">The list of strings</param>
        /// <param name = "tagsParameter">The tags that must be checked in the list</param>
        private static bool AreThereTagsInList(List<Tags> tags, params Tags[] tagsParameter)
        {
            Debug.Assert(tags != null && tagsParameter != null);

            if (tagsParameter.Length == 0)
            {
                return true;
            }

            if (tags.Count == 0)
            {
                return false;
            }
            bool areThereTags = false;

            foreach (Tags tagParameter in tagsParameter)
            {
                if (tags.BinarySearch(tagParameter) >= 0)
                {
                    areThereTags = true;
                }
                else
                {
                    areThereTags = false;
                    break;
                }
            }

            return areThereTags;
        }

        #endregion
    }
}
