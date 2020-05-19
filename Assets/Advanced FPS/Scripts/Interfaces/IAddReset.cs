using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddReset
{
    // Returns excess that is not added (including negative)
    int Add(int a);

    void Reset();
}
