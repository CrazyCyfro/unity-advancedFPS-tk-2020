using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransferable : IAddReset
{
    AmmoType Type();
    void Transfer(ITransferable target);
}
