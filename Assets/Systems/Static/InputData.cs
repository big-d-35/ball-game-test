using System;
using Unity.Mathematics;
using UnityEngine;

public static class InputData
{
    public static Action _startAiming;
    public static Action _disableAiming;
    public static Action<Vector2> _positing;
    public static Action<bool> _switchCam;

    public static void StartAiming()
    {
        _startAiming?.Invoke();
    }

    public static void DisableAiming()
    {
        _disableAiming?.Invoke();
    }

    public static void Aiming(Vector2 delta)
    {
        _positing?.Invoke(delta);
    }

    public static void SwitchCam(bool act)
    {
        _switchCam?.Invoke(act);
    }
}
