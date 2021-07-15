using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public Transform startOrientation = null;
    public Transform endOrientation = null;

    private MeshRenderer meshRenderer = null;
    private bool levelPulled = false;
    public UnityEvent leverPullEvent = new UnityEvent();
    public UnityEvent leverResetEvent = new UnityEvent();

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void OnLeverPullStart()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
    }
    public void OnLeverPullStop()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateLever(float percent)
    {
        transform.rotation = Quaternion.Slerp(startOrientation.rotation, endOrientation.rotation, percent);
        if (!levelPulled && percent >= .9f)
        {
            leverPullEvent?.Invoke();
            levelPulled = true;
        }
        else if(percent <= .9f)
        {
            leverResetEvent?.Invoke();
            levelPulled = false;
        }
    }
}