namespace it.amalfi.Pearl.input
{
    public interface IControllerReader
    {
        void ReadInput(ActionInput actionInput, params object[] valueInput);
        void AddInput();
        void RemoveInput();
    }
}