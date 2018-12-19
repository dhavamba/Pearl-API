using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.frameRate
{
    public class FrameRate : MonoBehaviour
    {
        #region Inspector Fields
        /// <summary>
        /// The refresh time is the time for calcolate the new frameRate
        /// </summary>
        [SerializeField]
        private float refreshTime = 0.5f;
        /// <summary>
        /// The limit frame rate is the upper limit for not making the game go too fast
        /// </summary>
        [SerializeField]
        private int limitFrameRate = 60;
        #endregion

        #region Public Fields
        /// <summary>
        /// OnFrameRate is a Event that is activated each time the frame rate is updated
        /// </summary>
        public static event EventHandlerSimple<int> OnFrameRate;
        #endregion

        #region Private Fields
        private int frameCounter = 0;
        private float timeCounter = 0.0f;
        private int lastFramerate = 0;
        #endregion

        #region Unity CallBacks
        private void Awake()
        {
            SettingLimitFrameRate();
        }

        private void Update()
        {
            CalculateFrameRate();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// This method set the limit of the desidered frame rate
        /// </summary>
        private void SettingLimitFrameRate()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = limitFrameRate;
        }

        /// <summary>
        /// This method calculate the actual frame rate 
        /// </summary>
        private void CalculateFrameRate()
        {
            if (this.timeCounter < this.refreshTime)
            {
                this.timeCounter += Time.deltaTime;
                this.frameCounter++;
            }
            else
            {
                this.lastFramerate = Mathf.RoundToInt((float)frameCounter / timeCounter);
                this.frameCounter = 0;
                this.timeCounter = 0.0f;
                OnFrameRate?.Invoke(lastFramerate);
            }
        }
        #endregion
    }
}
