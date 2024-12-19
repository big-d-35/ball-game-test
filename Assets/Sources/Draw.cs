using System.Collections.Generic;
using UnityEngine;
public interface IDraw
{
    void DrawTrajectory(bool isDraw);
}
public class Draw : MonoBehaviour, IDraw
{
    [SerializeField] private List<Transform> TrajectoryPoints = new List<Transform>();
    private LineRenderer lineRenderer;

    private float linePoints = 12;

    public void DrawTrajectory(bool isDraw)
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        if (!isDraw)
        {
            gameObject.SetActive(isDraw);
        }
        else
        {

            var points = new List<Vector3>();
            for (float ratio = 0; ratio <= 1; ratio += 1 / linePoints)
            {
                var targment1 = Vector3.Lerp(TrajectoryPoints[0].position, TrajectoryPoints[1].position, ratio);
                var targment2 = Vector3.Lerp(TrajectoryPoints[1].position, TrajectoryPoints[2].position, ratio);
                var curve = Vector3.Lerp(targment1, targment2, ratio);
                points.Add(curve);
            }
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        } 
    }
}
