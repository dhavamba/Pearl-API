using UnityEngine.UI;
using UnityEngine;

namespace it.amalfi.Pearl.frameRate
{
    [RequireComponent(typeof(Text))]
    public class FrameRateDebug : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private bool enableText;
        #endregion

        #region Private Fields
        private Text frameRateText;
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        void Awake()
        {
            frameRateText = GetComponent<Text>();
            frameRateText.enabled = enableText;
            FrameRate.OnFrameRate += SeeFPS;
        }

        private void OnDestroy()
        {
            FrameRate.OnFrameRate -= SeeFPS;
        }
        #endregion

        #region Private Methods
        private void SeeFPS(int FPS)
        {
            frameRateText.text = FPS.ToString() + " " + "FPS";
        }
        #endregion
    }
}
