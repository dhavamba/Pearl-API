using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.events;
using it.amalfi.Pearl;

namespace it.amalfi.Pearl.input
{
    public abstract class InputReaderManager : Singleton<InputReaderManager>
    {
        #region protected Fields
        protected StateInput stateInput;
        protected bool pause;
        #endregion

        #region private fields
        private const byte categoryInput = 1;
        private ControllerEnum controller = ControllerEnum.PC;
        private Dictionary<ActionInput, IControllerReader> inputReaders = new Dictionary<ActionInput, IControllerReader>();
        private const string nullable = "null";
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            EventsManager.OnAction += PauseControl;
        }

        private void Update()
        {
            stateInput = UpdateStateInput();
            switch (controller)
            {
                case ControllerEnum.PC:
                    UpdateKeyboard();
                    break;
                case ControllerEnum.JOYSTICK:
                    UpdateJoystick();
                    break;
            }
        }
        #endregion

        #region Public methods
        public static void Add(ActionInput actionInput, IControllerReader reader)
        {
            InputReaderManager obj = InputReaderManager.Instance;
            Debug.Assert(reader != null && !obj.inputReaders.ContainsKey(actionInput));
            obj.inputReaders.Update(actionInput, reader);
        }

        public static void Remove(ActionInput actionInput)
        {
            InputReaderManager.Instance.inputReaders.Remove(actionInput);
        }
        #endregion

        #region Protected Methods
        protected bool IsExist(ActionInput actionInput)
        {
            return inputReaders.ContainsKey(actionInput) && !inputReaders[actionInput].ToString().Equals(nullable);
        }

        protected void SetController(ControllerEnum controller)
        {
            this.controller = controller;
        }

        protected void ReadInput(ActionInput actionInput, params object[] valueInput)
        {
            if (IsExist(actionInput))
                inputReaders[actionInput].ReadInput(actionInput, valueInput);
        }
        #endregion

        #region Private Methods
        private void PauseControl(EventAction action, Dictionary<string, object> objects)
        {
            if (action == EventAction.CallPause)
                pause = (bool)objects["pause"];
        }
        #endregion

        #region Abstract Method
        protected abstract void UpdateKeyboard();
        protected abstract void UpdateJoystick();
        protected abstract StateInput UpdateStateInput();
        #endregion
    }
}
