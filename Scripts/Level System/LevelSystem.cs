using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using it.amalfi.Pearl.events;
using it.amalfi.Pearl;

namespace it.amalfi.Pearl.level
{
    /// <summary>
    /// A class that manages the scene change
    /// </summary>
    public abstract class LevelSystem
    {
        #region Public Fields
        public static event EventHandlerSimple<LevelEnum> OnNewLevel;
        #endregion

        #region Protected Fields
        protected Bictionary<LevelEnum, string> levelList;
        #endregion

        #region Constructors
        // Use this for initialization
        public LevelSystem()
        {
            ActiveLevelSystem();
            SetDictonary();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The method deatcive the level system
        /// </summary>
        public void DeactiveLevelSystem()
        {
            SceneManager.sceneLoaded -= ManageNewScene;
        }

        /// <summary>
        /// The method atcive the level system
        /// </summary>
        public void ActiveLevelSystem()
        {
            SceneManager.sceneLoaded += ManageNewScene;
        }

        /// <summary>
        /// The method accesses a new scene through the enumerator
        /// </summary>
        /// <param name = "newLevel">The new scene in enumerator</param>
        public void NewScene(LevelEnum newLevel)
        {
            SceneManager.LoadScene(levelList[newLevel]);
        }

        /// <summary>
        /// The method returns the level string from the enumerator
        /// </summary>
        /// <param name = "level">The level in enumeratorr</param>
        public string LevelToString(LevelEnum level)
        {
            return levelList[level];
        }

        /// <summary>
        /// The method returns enumerator of actual scene
        /// </summary>
        public LevelEnum ReturnLevel()
        {
            return GetActualLevel(SceneManager.GetActiveScene());
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// The method returns enumerator of actual scene, if the scene there is't, the method return null.
        /// </summary>
        private LevelEnum GetActualLevel(Scene scene)
        {
            try
            {
                return levelList[scene.name];
            }
            catch (KeyNotFoundException)
            {
                return LevelEnum.Null;
            }
        }

        /// <summary>
        /// the method is activated at the event "SceneManager.sceneLoaded": the method 
        /// activates itself to the event: it activates an event that indicates the
        /// beginning of a new scene.
        /// </summary>
        private void ManageNewScene(Scene scene, LoadSceneMode load)
        {
            OnNewLevel?.Invoke(GetActualLevel(scene));
        }
        #endregion

        #region Abstract Methods
        protected abstract void SetDictonary();
        #endregion
    }
}
