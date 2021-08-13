using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOverSceneChange : MonoBehaviour
{

    public bool applyToChildren = true;
    
    private int m_persistentLayer = 0;
    private int m_currentLayer = 0;

    private void Awake()
    {
        m_persistentLayer = LayerMask.NameToLayer("XR Persistent");
        m_currentLayer = gameObject.layer;
    }

    private void OnEnable()
    {
        SceneLoader.Instance.onLoadStart.AddListener(StartPersist);
        SceneLoader.Instance.onLoadFinish.AddListener(EndPersist);
    }

    private void OnDisable()
    {
        var loader = SceneLoader.Instance;
        if (loader != null)
        {
            SceneLoader.Instance.onLoadStart.RemoveListener(StartPersist);
            SceneLoader.Instance.onLoadFinish.RemoveListener(EndPersist);
        }
    }

    void StartPersist()
    {
        m_currentLayer = gameObject.layer;
        SetLayer(gameObject, m_persistentLayer, applyToChildren);
    }

    void EndPersist()
    {
        SetLayer(gameObject, m_currentLayer, applyToChildren);
    }

    void SetLayer(GameObject obj, int newLayer, bool applyTochildren)
    {
        obj.layer = newLayer;
        if (applyTochildren)
        {
            foreach (Transform child in obj.transform)
            {
                SetLayer(child.gameObject, newLayer, applyTochildren);
            }
        }
    }
    
}
