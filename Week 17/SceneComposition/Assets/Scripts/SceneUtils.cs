using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneUtils
{
    public static class Names
    {
        public static readonly string XRPersistent = "XRPersistant";
        public static readonly string Maze = "Maze";
        public static readonly string ComplexInteractions = "3DSwitchboard";
        public static readonly string Lobby = "Quitting Time Lobby";
    }

    public static void AlignXRRig(Scene persistentScene, Scene currentScene)
    {
        GameObject[] currentObjects = currentScene.GetRootGameObjects();
        GameObject[] persistentObjects = persistentScene.GetRootGameObjects();

        foreach (var origin in  currentObjects)
        {
            if (origin.CompareTag("XRRigOrigin"))
            {
                foreach (var rig in persistentObjects)
                {
                    if (rig.CompareTag("XRRig"))
                    {
                        rig.transform.position = origin.transform.position;
                        rig.transform.rotation = origin.transform.rotation;
                    }
                }
            }
        }
    }
}
