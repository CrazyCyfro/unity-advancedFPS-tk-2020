using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBleedable
{
    void Bleed(Vector3 point, Vector3 normal);
}
