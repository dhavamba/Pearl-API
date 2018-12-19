using UnityEngine;
using it.amalfi.Pearl.multitags;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.UI
{
    public class VisibilityPanelComponent : LogicalComponent<MessageUIMenuEnum>
    {
        public VisibilityPanelComponent(LogicalManager<MessageUIMenuEnum> manager) : base(manager)
        {
        }

        #region Public Methods
        public void Show(GameObject obj)
        {
            OpenCloseAllPanels(false);
            GameObject panel = FindPanelForSpecificUIObj(obj.transform)?.gameObject;
            if (!panel)
            {
                Debug.LogError("There isn't panel");
                return;
            }

            foreach (Transform child in manager.transform)
            {
                child.gameObject.SetActive(false);
            }
            panel.SetActive(true);
        }

        public void OpenCloseAllPanels(bool open)
        {
            foreach (Transform tr in manager.transform)
            {
                tr.gameObject.SetActive(open);
            }
        }
        #endregion

        #region Private Methods
        private Transform FindPanelForSpecificUIObj(Transform transform)
        {
            if (transform.gameObject.HaveTags(Tags.Panel))
                return transform;
            else if (transform.parent != null)
                return FindPanelForSpecificUIObj(transform.parent);
            else
                return null;
        }
        #endregion
    }
}
