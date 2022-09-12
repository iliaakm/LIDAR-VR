using System;
using System.Collections;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private float showTime = 0.1f;
    public LineRenderer LineRenderer;
    public Transform muzzle;

    public Action<Line> OnFired;

    private void OnEnable() => StartCoroutine(TimerCor());

    private void Update() => LineRenderer.SetPosition(0, muzzle.position);

    IEnumerator TimerCor()
    {
        yield return new WaitForSeconds(showTime);
        OnFired?.Invoke(this);
        gameObject.SetActive(false);
    }
}
