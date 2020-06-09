using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadable
{
    void PostFireAction();
    void Reload();
    void CancelReload();
}
