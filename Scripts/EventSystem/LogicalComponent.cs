using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl;
using System;
using it.amalfi.Pearl.events;

public abstract class LogicalComponent<T> where T : struct, IConvertible
{
    #region Protected Fields
    protected LogicalManager<T> manager;
    #endregion

    #region Constructors
    public LogicalComponent(LogicalManager<T> manager)
    {
        this.manager = manager;
    }
    #endregion

    #region Public Method
    public virtual void OnDestroy()
    {

    }
    #endregion

    #region Protected Methods
    protected static Tuple<string, object> KeyTuple(string s, object obj)
    {
        return TupleExtend.KeyTuple(s, obj);
    }
    #endregion
}
