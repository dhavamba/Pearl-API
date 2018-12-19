using it.amalfi.Pearl.events;
using System;

namespace it.amalfi.Pearl.input
{
    public abstract class InputReaderComponent<F> : LogicalComponent<F>, IControllerReader where F : struct, IConvertible
    {
        public InputReaderComponent(LogicalManager<F> manager) : base(manager)
        {
            AddInput();
        }

        public override void OnDestroy()
        {
            if (InputReaderManager.Instance)
                RemoveInput();
        }

        public abstract void ReadInput(ActionInput actionInput, params object[] valueInput);

        public abstract void AddInput();

        public abstract void RemoveInput();
    }
}
