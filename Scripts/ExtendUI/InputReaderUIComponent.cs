using System;
using System.Collections.Generic;
using it.amalfi.Pearl.input;
using UnityEngine;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.UI
{
    public class InputReaderUIComponent : InputReaderComponent<MessageUIMenuEnum>
    {
        public InputReaderUIComponent(LogicalManager<MessageUIMenuEnum> manager) : base(manager)
        {
        }

        public override void AddInput()
        {
            InputReaderManager.Add(ActionInput.UseReturnButton, this);
            InputReaderManager.Add(ActionInput.EntryMenu, this);
        }

        public override void ReadInput(ActionInput actionInput, params object[] valueInput)
        {
            if (actionInput == ActionInput.UseReturnButton)
                manager.DoComplexAction(MessageUIMenuEnum.GetInputReturn, valueInput);
            else if (actionInput == ActionInput.EntryMenu)
                manager.DoComplexAction(MessageUIMenuEnum.GetInputOpenCloseMenu, valueInput);
        }

        public override void RemoveInput()
        {
            InputReaderManager.Remove(ActionInput.UseReturnButton);
            InputReaderManager.Remove(ActionInput.EntryMenu);
        }
    }

}