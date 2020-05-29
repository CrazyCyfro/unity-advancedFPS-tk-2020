using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyInput
{

    public enum KeyState {
    Down,
    Hold,
    Up
    }   
    public KeyState keyState;
    public KeyCode keyCode;

    public bool KeyActive()
    {
        switch (keyState)
        {
            case KeyState.Down:
                if (Input.GetKeyDown(keyCode)) return true;
                break;
            case KeyState.Hold:
                if (Input.GetKey(keyCode)) return true;
                break;
            case KeyState.Up:
                if (Input.GetKeyUp(keyCode)) return true;
                break;
            default:
                return false;
        }

        return false;
    }
}
