using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace it.amalfi.Pearl.audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region Inspector Fields
        [SerializeField]
        private AudioMixer audioMixer;
        #endregion

        #region Private Fields
        private AudioMixerSnapshot pause;
        private AudioMixerSnapshot notPause;
        private static readonly Range rangeAudioDb = new Range(-30, 10);
        private float volumeMixer;

        private Dictionary<string, AudioContainer> containers;
        private List<AudioContainer> activeContainer;
        #endregion

        #region Unity CallBacks
        private void Awake()
        {
            pause = audioMixer.FindSnapshot("Pause");
            notPause = audioMixer.FindSnapshot("UnPause");

            containers = new Dictionary<string, AudioContainer>
        {
            { "musicVolume", new AudioContainer("musicVolume") },
            { "soundEffectVolume", new AudioContainer("soundEffectVolume") },
            { "masterVolume", new AudioContainer("masterVolume") }
        };

            activeContainer = new List<AudioContainer>();
        }

        private void Update()
        {
            ExecuteChangeVolume();
        }
        #endregion

        #region Public Methods
        public void Pause(float time)
        {
            Debug.Assert(time >= 0);
            pause.TransitionTo(time);
        }

        public void UnPause(float time)
        {
            Debug.Assert(time >= 0);
            notPause.TransitionTo(time);
        }

        public void SetMusicVolume(float value, float time = 0, AnimationCurve curve = null)
        {
            Debug.Assert(value >= 0 && value <= 1 && time >= 0);
            SetVolume("musicVolume", value, time, curve);
        }

        public float GetMusicVolume()
        {
            return GetVolume("musicVolume");
        }

        public void SetSoundEffectVolume(float value, float time = 0, AnimationCurve curve = null)
        {
            Debug.Assert(value >= 0 && value <= 1 && time >= 0);
            SetVolume("soundEffectVolume", value, time, curve);
        }

        public float GetSoundEffectVolume()
        {
            return GetVolume("soundEffectVolume");
        }

        public void SetMasterVolume(float value, float time = 0, AnimationCurve curve = null)
        {
            Debug.Assert(value >= 0 && value <= 1 && time >= 0);
            SetVolume("masterVolume", value, time, curve);
        }

        public float GetMasterVolume()
        {
            return GetVolume("soundEffectVolume");
        }
        #endregion

        #region Private Methods
        private float GetVolume(string nameGroup)
        {
            audioMixer.GetFloat(nameGroup, out volumeMixer);
            volumeMixer = MathfExtend.Percent(volumeMixer, rangeAudioDb);
            return volumeMixer;
        }

        public void SetVolume(string nameGroup, float value, float time = 0, AnimationCurve curve = null)
        {
            Debug.Assert(value >= 0 && value <= 1 && time >= 0);

            value = MathfExtend.ChangeRange(value, rangeAudioDb);
            audioMixer.GetFloat(nameGroup, out volumeMixer);

            if (value != volumeMixer)
            {
                if (time != 0)
                {
                    volumeMixer = containers[nameGroup].Reset(volumeMixer, value, time, curve);
                    activeContainer.Add(containers[nameGroup]);
                }
                else
                    volumeMixer = value;
                audioMixer.SetFloat(nameGroup, volumeMixer);
            }
        }

        private void ExecuteChangeVolume()
        {
            for (int i = activeContainer.Count - 1; i >= 0; i--)
            {
                if (!activeContainer[i].IsFinish())
                    audioMixer.SetFloat(activeContainer[i].Name, activeContainer[i].ReturnVolume());
                else
                    activeContainer.RemoveAt(i);
            }
        }
        #endregion
    }
}
