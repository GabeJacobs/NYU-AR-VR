using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent (typeof(Light))]
 
public class StrobeLight : MonoBehaviour
{

    public Light light = null;
    public float time = .5f; //time between on and off
    private Coroutine m_strobe = null;

    private void Awake()
    {
        light = GetComponent<Light>();
    }
    public void StartStrobe()
    {
        if(m_strobe != null)
            StopCoroutine(m_strobe);
        m_strobe = StartCoroutine(routine: Flicker());
    }

    public void StopStrobe()
    {
        if (m_strobe != null)
        {
            StopCoroutine(m_strobe);
            m_strobe = null;

            light.enabled = true;
            light.color = Color.white;
        }
    }
    
    public void toggleStrobe()
    {
        if (m_strobe != null)
        {
            StopStrobe();
        }
        else
        {
            StartStrobe();
        }
    }
   
    IEnumerator Flicker() {
        while(true) {
            light.enabled = !light.enabled;
            if (light.enabled)
            {
                light.color = new Color(Random.value, Random.value, Random.value);
            }
            yield return new WaitForSeconds(time);
        }
    }
}