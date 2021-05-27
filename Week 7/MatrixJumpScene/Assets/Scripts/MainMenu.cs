using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
