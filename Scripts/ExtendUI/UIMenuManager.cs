using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using it.amalfi.Pearl.audio;
using it.amalfi.Pearl.events;
using System;
using it.amalfi.Pearl.input;

namespace it.amalfi.Pearl.UI
{
    public abstract class UIMenuManager : LogicalManager<MessageUIMenuEnum>
    {
        #region Inspector Fields
        [SerializeField]
        protected GameObject firstUIObjectEnable;
        [SerializeField]
        private StateUI stateUI;
        #endregion

        #region Protected Fields
        protected bool isOpenUI = false;
        #endregion

        #region Private Fields
        private enum StateUI { Menu, Pause }
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        private void Start()
        {
            SetSpecificUI();
        }
        #endregion

        #region Override Methods
        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent<MessageUIMenuEnum>>
            {
                { typeof(VisibilityPanelComponent), new VisibilityPanelComponent(this) },
                { typeof(SelectionPanelComponent), new SelectionPanelComponent(this) },
                { typeof(InputReaderComponent<MessageUIMenuEnum>), new InputReaderUIComponent(this) },
            };
        }

        protected override void AddActionWhenReceiveEvent()
        {
            receiveEvent.Add(EventAction.CallPause, delegate (Dictionary<string, object> objects)
            {
                OpenMenu((bool)objects["pause"]);
            });
        }

        public override void DoComplexAction(MessageUIMenuEnum enumerator, params object[] objects)
        {
            MessageUIMenuEnum choose = (MessageUIMenuEnum)(object)enumerator;
            switch (choose)
            {
                case MessageUIMenuEnum.ChangePanel:
                    GetLogicalComponent<VisibilityPanelComponent>().Show((GameObject)objects[0]);
                    GetLogicalComponent<SelectionPanelComponent>().ChangeSelectNext((GameObject)objects[0]);
                    break;
                case MessageUIMenuEnum.CloseMenu:
                    GetLogicalComponent<VisibilityPanelComponent>().OpenCloseAllPanels(false);
                    GetLogicalComponent<SelectionPanelComponent>().Reset();
                    break;
                case MessageUIMenuEnum.ChangeButton:
                    GetLogicalComponent<SelectionPanelComponent>().ChangeSelectNext((GameObject)objects[0]);
                    break;
                case MessageUIMenuEnum.GetInputOpenCloseMenu:
                    if (stateUI == StateUI.Pause)
                        SendOut(EventAction.CallPause, TupleExtend.KeyTuple("pause", !isOpenUI));
                    break;
                case MessageUIMenuEnum.GetInputReturn:
                    if (isOpenUI)
                    {
                        if (!GetLogicalComponent<SelectionPanelComponent>().IsOpenPage)
                        {
                            GetLogicalComponent<VisibilityPanelComponent>().Show(GetLogicalComponent<SelectionPanelComponent>().PreSelect);
                            GetLogicalComponent<SelectionPanelComponent>().ChangeInPreSelect();
                        }
                        else if (stateUI == StateUI.Pause)
                            SendOut(EventAction.CallPause, TupleExtend.KeyTuple("pause", !isOpenUI));
                    }
                    break;
            }
        }

        public override void SendOut(EventAction message, params Tuple<string, object>[] objects)
        {
            switch (message)
            {
                case EventAction.CallPause:
                    EventsManager.CallEventLocal(EventAction.CallPause, objects);
                    break;
            }
        }
        #endregion

        #region Public Methods
        public void ChangePanel(GameObject obj)
        {
            DoComplexAction(MessageUIMenuEnum.ChangePanel, obj);
        }

        public void ChangeButton(GameObject obj)
        {
            DoComplexAction(MessageUIMenuEnum.ChangeButton, obj);
        }

        public void Quit()
        {
            #if UNITY_STANDALONE
                Application.Quit();
            #endif

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        #endregion

        #region Private Methods
        private void OpenMenu(bool isOpenUI)
        {
            this.isOpenUI = isOpenUI;
            if (!isOpenUI)
                DoComplexAction(MessageUIMenuEnum.CloseMenu);
            else
                DoComplexAction(MessageUIMenuEnum.ChangePanel, firstUIObjectEnable);
        }
        #endregion

        #region Abstract Method
        protected abstract void SetSpecificUI();
        #endregion
    }
}
