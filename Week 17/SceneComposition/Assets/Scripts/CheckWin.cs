using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckWin : MonoBehaviour
{
    public UnityEvent onWin = new UnityEvent();
    public GameObject winText;
    
    private void Start()
    {
        winText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            onWin?.Invoke();
            if(!winText.activeInHierarchy)
                winText.SetActive(true);
        }
    }
}
