using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

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
    public override void OnInspectorGUI() {
        if(GUILayout.Button("Open Editor")) {
            EventEditorWindow.Open((Event) target);
        }
    }
}



[CustomPropertyDrawer(typeof(EventScreen))]
public class EventScreenCustomEditor : PropertyDrawer {

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var titleField = new PropertyField(property.FindPropertyRelative("title"));
        var titleField2 = new PropertyField(property.FindPropertyRelative("title"));
        

        // Add fields to the contai
        container.Add(titleField);
        return container;
    }

}