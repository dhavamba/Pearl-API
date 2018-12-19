using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.events
{
    /// <summary>
    /// A event handler with four parameter generic
    /// </summary>
    /// <param name = "parm1"> This first parameter of generic method</param>
    /// <param name = "parm2"> This second parameter of generic method</param>
    /// <param name = "parm3"> This third parameter of generic method</param>
    /// <param name = "parm4"> This fourth parameter of generic method</param>
    public delegate void EventHandlerSimple<T, F, R, H>(T parm1, F parm2, R parm3, H parm4);

    /// <summary>
    /// A event handler with three parameter generic
    /// </summary>
    /// <param name = "parm1"> This first parameter of generic method</param>
    /// <param name = "parm2"> This second parameter of generic method</param>
    /// <param name = "parm3"> This third parameter of generic method</param>
    public delegate void EventHandlerSimple<T, F, R>(T parm1, F parm2, R parm3);

    /// <summary>
    /// A event handler with two parameter generic
    /// </summary>
    /// <param name = "parm1"> This first parameter of generic method</param>
    /// <param name = "parm2"> This second parameter of generic method</param>
    public delegate void EventHandlerSimple<T, F>(T parm1, F parm2);

    /// <summary>
    /// A event handler with a parameter generic
    /// </summary>
    /// <param name = "parm1"> This first parameter of generic method</param>
    public delegate void EventHandlerSimple<T>(T parm);

    /// <summary>
    /// A event handler with no parameter very generic
    /// </summary>
    public delegate void EventHandlerSimple();
}