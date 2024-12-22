using UnityEngine;
using System.Collections.Generic;

public class BallCameraController : MonoBehaviour
{
    [System.Serializable]
    public class CameraPosition
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    [Header("Camera Positions")]
    public List<CameraPosition> positions = new List<CameraPosition>();

    [Header("Settings")]
    public float transitionSpeed = 5f;
    public bool useSmoothing = true;

    private int currentPositionIndex = 0;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (positions.Count == 0)
        {
            Debug.LogWarning("No camera positions defined!");
        }
    }

    void Update()
    {
        if (positions.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentPositionIndex--;
            if (currentPositionIndex < 0)
                currentPositionIndex = positions.Count - 1;

            MoveCameraToCurrentPosition();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positions.Count)
                currentPositionIndex = 0;

            MoveCameraToCurrentPosition();
        }

        if (useSmoothing)
        {
            SmoothUpdateCameraPosition();
        }
    }

    void MoveCameraToCurrentPosition()
    {
        if (!useSmoothing)
        {
            mainCamera.transform.position = positions[currentPositionIndex].position;
            mainCamera.transform.eulerAngles = positions[currentPositionIndex].rotation;
        }
    }

    void SmoothUpdateCameraPosition()
    {
        Vector3 targetPosition = positions[currentPositionIndex].position;
        Vector3 targetRotation = positions[currentPositionIndex].rotation;

        mainCamera.transform.position = Vector3.Lerp(
            mainCamera.transform.position,
            targetPosition,
            Time.deltaTime * transitionSpeed
        );

        mainCamera.transform.rotation = Quaternion.Lerp(
            mainCamera.transform.rotation,
            Quaternion.Euler(targetRotation),
            Time.deltaTime * transitionSpeed
        );
    }
}