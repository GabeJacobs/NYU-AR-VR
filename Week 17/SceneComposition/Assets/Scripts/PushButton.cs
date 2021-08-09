using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButton : MonoBehaviour
{
    [Min(0.01f)]
    public float depressionDepth = .015f;
    private float m_yMax = 0.0f; // resting position
    private float m_yMin = 0.0f; // all the way pressed in
    private float m_currentPressDepth = 0.0f;
    [Min(0.0001f)]
    public float pressThreshold = 0.001f;
    
    [Min(0.0001f)]
    public float resetThreshold = 0.001f; 
    
    private List<Collider> m_currentColliders = new List<Collider>();
    private XRBaseInteractor m_interactor = null;
    private bool wasPressed = false;
    
    public UnityEvent onPressed = new UnityEvent();
    public UnityEvent onReset = new UnityEvent();
    public UnityEvent onInteractionStart = new UnityEvent();
    public UnityEvent onInteractionEnd = new UnityEvent();

    [Min(0.01f)]
    public float returnSpeed = 1;
    private float GetPressDepth(Vector3 interactorWorldPosition)
    {
        return transform.parent.InverseTransformPoint(interactorWorldPosition).y;
    }
    private void OnTriggerEnter(Collider other)
    {
        XRBaseInteractor interactor = other.GetComponentInParent<XRBaseInteractor>();
        if (interactor != null && !other.isTrigger)
        {
            m_currentColliders.Add(other);
            if (m_interactor == null)
            {
                m_interactor = interactor;
                SetMinRange();
                m_currentPressDepth = GetPressDepth(m_interactor.transform.position);
                onInteractionStart?.Invoke();                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_currentColliders.Contains(other))
        {
            m_currentColliders.Remove(other);
            if (m_currentColliders.Count == 0)
            {
                onInteractionEnd?.Invoke();
                EndPress();
            }
        }
    }

    void EndPress()
    {
        m_currentColliders.Clear();
        m_currentPressDepth = 0;
        m_interactor = null;
    }

    bool isPressed()
    {
        return transform.localPosition.y >= m_yMin && transform.localPosition.y <= m_yMin + pressThreshold;
    }

    bool isReset()
    {
        return (transform.localPosition.y >= m_yMax - resetThreshold && transform.localPosition.y <= m_yMax);
    }
    void SetMinRange()
    {
        m_yMin = m_yMax - depressionDepth;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_yMax = transform.localPosition.y;
    }

    void SetHeight(float newHeight)
    {
        Vector3 currentPosition = transform.localPosition;
        currentPosition.y = newHeight;
        currentPosition.y = Mathf.Clamp(currentPosition.y, m_yMin, m_yMax);
        transform.localPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_interactor != null)
        {
            float newPressHeight = GetPressDepth(m_interactor.transform.position);
            float deltaHeight = m_currentPressDepth - newPressHeight;
            float newPressedPosition = transform.localPosition.y - deltaHeight;
            SetHeight(newPressedPosition);

            if (!wasPressed && isPressed())
            {
                onPressed?.Invoke();
                wasPressed = true;
                // we pressed the button
            }

            m_currentPressDepth = newPressHeight;
        }
        else
        {
            if (!Mathf.Approximately(transform.localPosition.y, m_yMax))
            {
                float returnHeight = Mathf.MoveTowards(transform.localPosition.y, m_yMax, Time.deltaTime * returnSpeed);
                SetHeight(returnHeight);
            }
        }

        if (wasPressed && isReset())
        {
            wasPressed = false;
            onReset?.Invoke();
        }
    }
}
