using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadStatus
{
    bool OutOfAmmo();
    bool Reloading();
    string AmmoString();

}
