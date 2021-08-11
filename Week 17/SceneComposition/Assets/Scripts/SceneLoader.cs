using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class SceneLoader : Singleton<SceneLoader>
{
    public Material screenFade = null;
    public UnityEvent onLoadStart = new UnityEvent();
    public UnityEvent onLoadFinish = new UnityEvent();
    public UnityEvent onBeforeUnload = new UnityEvent();
    
    [Min(0.001f)]
    public float fadeSpeed = 1.0f;
    
    [Range (0.0f, 5.0f)]
    public float addedWaitTime = 2.0f;
    
    private bool m_isLoading = false;
    private float m_fadeAmount = 0.0f;
    private Coroutine m_fadeCoroutine = null;
    private static readonly int m_fadeAmountPropID = Shader.PropertyToID("_FadeAmount");

    private Scene m_persistantScene;
    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;
        m_persistantScene = SceneManager.GetActiveScene();

        if (!Application.isEditor)
        {
            SceneManager.LoadSceneAsync(SceneUtils.Names.Lobby,LoadSceneMode.Additive);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;
    }

    public void LoadScene(string name)
    {
        if (!m_isLoading)
        {
            StartCoroutine(Load(name));
        } 
    }
    
    void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        SceneUtils.AlignXRRig( m_persistantScene, scene);
    }
    
    IEnumerator Load(string name)
    {
        m_isLoading = true;
        onLoadStart?.Invoke();;
        yield return FadeOut();
        onBeforeUnload?.Invoke();
        yield return new WaitForSeconds(0);
        yield return StartCoroutine(UnloadCurrentScene());
        yield return new WaitForSeconds(addedWaitTime);
        yield return StartCoroutine(LoadNewScene(name));
        yield return FadeIn();
        onLoadFinish?.Invoke();
        m_isLoading = false;

    }
    
    IEnumerator UnloadCurrentScene()
    {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!unload.isDone)
        {
            yield return null;
        }
        
    }
    
    IEnumerator LoadNewScene(string name)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
    }
    
    IEnumerator FadeOut()
    {
        if (m_fadeCoroutine != null)
        {
            StopCoroutine(m_fadeCoroutine);
        }
        m_fadeCoroutine = StartCoroutine(Fade(1.0f));
        yield return m_fadeCoroutine;
    }
    
    IEnumerator FadeIn()
    {
        if (m_fadeCoroutine != null)
        {
            StopCoroutine(m_fadeCoroutine);
        }
        m_fadeCoroutine = StartCoroutine(Fade(0.0f));
        yield return m_fadeCoroutine;
    }

    IEnumerator Fade(float target)
    {
        while (!Mathf.Approximately(m_fadeAmount, target))
        {
            m_fadeAmount = Mathf.MoveTowards(m_fadeAmount, target, fadeSpeed * Time.deltaTime);
            screenFade.SetFloat(m_fadeAmountPropID, m_fadeAmount);
            yield return null;
        }
        screenFade.SetFloat(m_fadeAmountPropID, m_fadeAmount);
    }
}
