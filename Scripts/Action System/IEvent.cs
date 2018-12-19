using System.Collections.Generic;
using it.amalfi.Pearl.multitags;

namespace it.amalfi.Pearl.actionTrigger
{
    public interface IEvent
    {
        void Trigger(Informations informations, List<Tags> tags);
    }
}
