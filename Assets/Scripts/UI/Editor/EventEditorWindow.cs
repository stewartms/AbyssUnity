using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class EventEditorWindow : ExtendedEditorWindow
{

    private ReorderableList list;

    public static void Open(Event openedEvent) 
    {
        EventEditorWindow window =  GetWindow<EventEditorWindow>("Event Editor");
        window.serializedObject = new SerializedObject(openedEvent);
        
        window.list = new ReorderableList (window.serializedObject,
			window.serializedObject.FindProperty ("eventScreens"),
			true, true, true, true);

        
	
		window.list.drawElementCallback = 
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = window.list.serializedProperty.GetArrayElementAtIndex (index);
			rect.y += 2; 
            EditorGUI.PropertyField(
                new Rect( rect.x-150, rect.y, rect.width+130, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("title"));
                GUILayout.Button("Edit " + element.FindPropertyRelative("title"));
                /*
			EditorGUI.PropertyField (
				new Rect (rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight),
				element, new GUIContent("Event Screen"));*/
	
		};
    }

    private void OnGUI() {
        currentProperty = serializedObject.FindProperty("eventScreens");
        
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));

        list.DoLayoutList();
        //DrawSidebar(currentProperty);

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if(selectedProperty != null) {
            if(GUILayout.Button("Delete")) {
                //List<EventScreen> list = (List<EventScreen>) serializedObject.FindProperty("eventScreens");
                //list.RemoveAt(0);
            }
            DrawSelectedPropertiesPanel();
        } else {
            EditorGUILayout.LabelField("Select an Event Screen from the left.");
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();
    }

    void DrawSelectedPropertiesPanel() {
        

        currentProperty = selectedProperty;

        DrawField("title", true);
        DrawField("text", true);

    }


}
