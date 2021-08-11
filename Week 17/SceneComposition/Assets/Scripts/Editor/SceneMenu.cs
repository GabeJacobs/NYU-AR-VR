using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;

public static class SceneMenu
{
    [MenuItem("Scenes/Lobby")]
    private static void OpenLobby()
    {
        OpenScene(SceneUtils.Names.Lobby);
    }
    [MenuItem("Scenes/Maze")]
    private static void OpenMaze()
    {
        OpenScene(SceneUtils.Names.Maze);
    }
    [MenuItem("Scenes/ComplexInteractions")]
    private static void OpenComplexInteractions()
    {
        OpenScene(SceneUtils.Names.ComplexInteractions);
    }
    private static void OpenScene(string name)
    {
        Scene persistantScene = EditorSceneManager.OpenScene("Assets/Scenes/" + SceneUtils.Names.XRPersistent + ".unity", OpenSceneMode.Single);
        Scene currentScene = EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity", OpenSceneMode.Additive);
        
        SceneUtils.AlignXRRig(persistantScene,currentScene);
    }
}
