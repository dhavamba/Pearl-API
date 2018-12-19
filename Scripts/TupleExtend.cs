using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace it.amalfi.Pearl
{
    public static class TupleExtend
    {
        public static Tuple<string, object> KeyTuple(string s, object obj)
        {
            return new Tuple<string, object>(s, obj);
        }
    }
}
