using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.UI
{
    public class SelectionPanelComponent : LogicalComponent<MessageUIMenuEnum>
    {
        #region Private Fields
        private Stack<GameObject> cascadeUI;
        #endregion

        #region Property
        public GameObject PreSelect
        {
            get
            {
                if (cascadeUI.Count == 0)
                    return null;
                return cascadeUI.Peek();
            }
        }

        public bool IsOpenPage
        {
            get
            {
                return PreSelect == null;
            }
        }
        #endregion

        #region Constructors
        // Use this for initialization
        public SelectionPanelComponent(LogicalManager<MessageUIMenuEnum> manager) : base(manager)
        {
            cascadeUI = new Stack<GameObject>();
        }
        #endregion

        #region Public Methods
        public void ChangeSelectNext(GameObject obj)
        {
            if (obj != EventSystem.current.currentSelectedGameObject)
            {
                cascadeUI.Push(EventSystem.current.currentSelectedGameObject);
                EventSystem.current.SetSelectedGameObject(obj);
            }
        }

        public void Reset()
        {
            EventSystem.current?.SetSelectedGameObject(null);
            cascadeUI.Clear();
        }

        public void ChangeInPreSelect()
        {
            if (cascadeUI.Count != 0)
                EventSystem.current.SetSelectedGameObject(cascadeUI.Pop());
        }
        #endregion
    }
}