using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    public LineRenderer line;
    [SerializeField] private int circleSteps;
    [SerializeField] private float circleRadius;
    // Start is called before the first frame update
    void Start()
    {
        DrawCircle(circleSteps, circleRadius);
    }

    void DrawCircle(int steps, float radius)
    {
        line.positionCount = steps+1;
        float progress;
        float ang;
        float x, y;
        for (int i = 0; i <= steps; i++)
        {
            progress = (float)i / steps;
            ang = progress * 2 * Mathf.PI;
            x = Mathf.Cos(ang) * radius;
            y = Mathf.Sin(ang) * radius;
            Vector3 currPos = new Vector3(x, 0, y);
            line.SetPosition(i, transform.position+currPos);
        }
    }
}
