using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab = null;
    [SerializeField] private Transform muzzle;

    private List<Line> lines = new List<Line>(200);

    void Start()
    {
        for (int i = 0; i < lines.Capacity; i++)
            AddLineToPool();
    }

    private void AddLineToPool()
    {
        var line = Instantiate(linePrefab);
        lines.Add(line);
        line.muzzle = muzzle;
        line.LineRenderer.positionCount = 2;
        line.gameObject.SetActive(false);
        line.OnFired += delegate(Line line1) { lines.Add(line); };
    }

    public void DrawLine(Vector3 pos1, Vector3 pos2)
    {
        if(lines.Count == 0)
            AddLineToPool();
        
        var line = lines[0];
        lines.RemoveAt(0);
        line.gameObject.SetActive(true);
        line.LineRenderer.SetPosition(0, pos1);
        line.LineRenderer.SetPosition(1, pos2);
    }
}
