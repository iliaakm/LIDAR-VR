using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab = null;
    [SerializeField] private Transform muzzle;

    public void DrawLine(Vector3 pos1, Vector3 pos2)
    {
        var line = Instantiate(linePrefab);
        line.LineRenderer.positionCount = 2;
        line.LineRenderer.SetPosition(0, pos1);
        line.LineRenderer.SetPosition(1, pos2);
        line.muzzle = muzzle;
    }
}
