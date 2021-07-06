using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{

    public GameObject winText;
    
    private void Start()
    {
        winText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if(!winText.activeInHierarchy)
                winText.SetActive(true);
        }
    }
}
