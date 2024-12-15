using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Input : SystemBase
{
    private InputAction aim;
    private Vector2 delta;

    protected override void OnStartRunning()
    {
        aim = new InputAction("takeAim", binding: "<Gamepad>/RightStick");
        aim.performed += context => { delta = context.ReadValue<Vector2>(); InputData.Aiming(delta); };
        aim.Enable();
    }

    protected override void OnStopRunning()
    {
        aim.Disable();
    }
    protected override void OnUpdate()
    {

    }
}
