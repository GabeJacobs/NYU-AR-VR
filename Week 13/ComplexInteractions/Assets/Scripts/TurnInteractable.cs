using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

[Serializable]
public class TurnEvent : UnityEvent<float>
{
}

public class TurnInteractable : XRBaseInteractable
{
    private XRBaseInteractor m_interactor = null;

    private Coroutine m_turn = null;

    [HideInInspector]
    private float turnAngle = 0.0f;
    private Vector3 m_startingRotation = Vector3.zero;

    public UnityEvent onTurnStart = new UnityEvent();
    public UnityEvent onTurnStop = new UnityEvent();
    public TurnEvent onTurnUpdate = new TurnEvent();
    // Start is called before the first frame update


    Quaternion GetLocalRotation(Quaternion targetWorld)
    {
        return Quaternion.Inverse(targetWorld) * transform.rotation;
    }
    void StartTurn()
    {
        if(m_turn != null)
            StopCoroutine(m_turn);
        Quaternion localRotation = GetLocalRotation(m_interactor.transform.rotation);
        m_startingRotation = localRotation.eulerAngles;
        onTurnStart?.Invoke();
        m_turn = StartCoroutine(UpdateTurn());
    }

    void StopTurn()
    {
        if (m_turn != null)
        {
            StopCoroutine(m_turn);
            onTurnStop?.Invoke();

            m_turn = null;
        }
    }

    IEnumerator UpdateTurn()
    {
        while (m_interactor != null)
        {
            Quaternion localRotation = GetLocalRotation(m_interactor.transform.rotation);
            turnAngle = m_startingRotation.z - localRotation.eulerAngles.z;
            onTurnUpdate?.Invoke(turnAngle);
            yield return null;
        }
    }
    
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        m_interactor = interactor;
        StartTurn();
        base.OnSelectEntered(interactor);
    }
    
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        StopTurn();
        m_interactor = null;
        base.OnSelectExited(interactor);
    }
}
