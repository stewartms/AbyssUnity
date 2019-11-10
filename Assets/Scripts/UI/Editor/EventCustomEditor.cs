using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler {
    public static bool OpenEditor(int instanceID, int line) {

        Event e = EditorUtility.InstanceIDToObject(instanceID) as Event;
        if(e != null) {
            EventEditorWindow.Open(e);
        }
        return false;
    }
}

[CustomEditor(typeof(Event))]
public class EventCustomEditor : Editor
{

    bool showDefault;

    public override void OnInspectorGUI() {

        if(GUILayout.Button("Open Editor")) {
            EventEditorWindow.Open((Event) target);
        }

        GUILayout.Label("DEBUGGER");
        showDefault = EditorGUILayout.Foldout(showDefault, "Show default inspector for debugging.");
        if(showDefault) {
            DrawDefaultInspector();
        }
    }
}

