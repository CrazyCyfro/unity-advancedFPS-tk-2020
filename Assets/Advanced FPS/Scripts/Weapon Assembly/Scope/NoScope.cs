using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoScope : ScopeBase, IScopeStatus
{
    public bool NoCrosshair()
    {
        return false;
    }

    public override void Scope(){}

    public bool Scoped()
    {
        return false;
    }
}
