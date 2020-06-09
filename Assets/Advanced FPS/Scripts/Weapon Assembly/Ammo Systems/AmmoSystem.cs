using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AmmoSystem : MonoBehaviour
{
    public abstract bool CanFire();
    public abstract void Fired();
}
