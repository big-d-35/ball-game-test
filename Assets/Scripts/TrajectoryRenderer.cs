using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private int valuePoints = 100;
    private LineRenderer lineRendererComponent;

    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();
    }

   public void ShowTrajectory(Vector3 origin, Vector3 speed)
	{
		int valuePoints = 100; // Количество точек для отрисовки траектории
		Vector3[] points = new Vector3[valuePoints];
		lineRendererComponent.positionCount = points.Length;

		for (int i = 0; i < points.Length; i++)
		{
			float time = i * 0.1f;

			points[i] = origin + speed * time + 0.5f * Physics.gravity * time * time;

			if (points[i].y < 0)
			{
				lineRendererComponent.positionCount = i + 1;
				break;
			}
		}
		lineRendererComponent.SetPositions(points);
	}
}