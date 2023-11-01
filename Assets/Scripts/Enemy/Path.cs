using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class Path : MonoBehaviour
{
    [SerializeField] private CircalArea spawnZone;


    [SerializeField] private AI_PatrolZone[] points;

    public int Length { get => points.Length; }

    public CircalArea StartArea { get { return spawnZone; } }

    public AI_PatrolZone this[int i] { get => points[i]; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(point.transform.position, point.Radius);
        }
    }
}

