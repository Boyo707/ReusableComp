using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PathFGizmoShit : MonoBehaviour
{
    [SerializeField]
    private Transform[] _patrolPoints;

    [SerializeField] private bool _connected;

    private void OnDrawGizmos()
    {
        DrawPoints();
        DrawPaths();
    }

    private void DrawPoints()
    {
        Gizmos.color = Color.yellow;
        foreach(Transform points in _patrolPoints)
        {
            if (points != null)
                Gizmos.DrawSphere(points.position, .25f);
        }
    }

    private void DrawPaths()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < _patrolPoints.Length-1; i++)
        {
            if (_patrolPoints[i] != null && _patrolPoints[i + 1] != null)
            {
                Vector3 thisPoint = _patrolPoints[i].position;
                Vector3 nextPoint = _patrolPoints[i + 1].position;
                Gizmos.DrawLine(thisPoint, nextPoint);
            }
        }
    }
}
