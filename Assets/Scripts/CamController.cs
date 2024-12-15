using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _secondaryCam;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject InputStick;

    private void Start()
    {
        InputData._switchCam += SwitchCam;
    }

    public void SwitchCam(bool act)
    {
        if (act)
            _secondaryCam.Priority = 2;
        else
            _secondaryCam.Priority = 0;
        RestartButton.SetActive(act);
        InputStick.SetActive(!act);
    }
}
