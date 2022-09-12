using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private float showTime = 0.1f;
    public LineRenderer LineRenderer;
    public Transform muzzle;
    
    private void OnEnable() => StartCoroutine(TimerCor());

    private void Update() => LineRenderer.SetPosition(0, muzzle.position);

    IEnumerator TimerCor()
    {
        yield return new WaitForSeconds(showTime);
        gameObject.SetActive(false);
    }
}
