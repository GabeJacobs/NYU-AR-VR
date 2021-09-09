// A simple script that saves frames from the Game view when in Play mode.
//
// You can put the frames together later to create a video.
// The frames are saved in the project, at the same level of the project hierarchy as the Assets folder.

using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine.Rendering.HighDefinition;

public class LightLayerManager : EditorWindow
{
    LightLayerEnum m_lightLayerMask = 0;

    [MenuItem("GameObject/Manage Light Layers")]
    static void Init()
    {
        LightLayerManager window =
            (LightLayerManager)EditorWindow.GetWindow(typeof(LightLayerManager));
    }

    void OnGUI()
    {
        m_lightLayerMask = (LightLayerEnum)EditorGUILayout.EnumFlagsField("Light Layer Mask", m_lightLayerMask);

        GUILayout.Label("Selected: " + Selection.count);
        if (Selection.count == 0) GUI.enabled = false;
        if (GUILayout.Button("Set Selected to Light Layer Mask"))
        {
            foreach(var go in Selection.gameObjects)
            {
                Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
                if (renderers == null) continue;
                foreach(Renderer renderer in renderers)
                {
                    renderer.renderingLayerMask = (uint)m_lightLayerMask;
                    EditorUtility.SetDirty(renderer);
                }
            }
        }
        if (Selection.count == 0) GUI.enabled = true;
    }
}