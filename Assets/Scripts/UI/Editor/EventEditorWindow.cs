using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class EventEditorWindow : ExtendedEditorWindow
{

    private ReorderableList eventScreenList;
    private Dictionary<string, ReorderableList> optionListDict = new Dictionary<string, ReorderableList>();

    public static void Open(Event openedEvent) 
    {
        EventEditorWindow window =  GetWindow<EventEditorWindow>("Event Editor");
        window.serializedObject = new SerializedObject(openedEvent);
        
        window.eventScreenList = new ReorderableList (window.serializedObject,
			window.serializedObject.FindProperty ("eventScreens"),
			true, true, true, true);

        window.eventScreenList.drawHeaderCallback = rect => {
            EditorGUI.LabelField(rect, "Event Screens");
        };

        /*
		window.list.drawElementCallback = 
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = window.list.serializedProperty.GetArrayElementAtIndex (index);
			rect.y += 2; 
            EditorGUI.PropertyField(
                new Rect( rect.x-150, rect.y, rect.width+130, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("title"));
                
			EditorGUI.PropertyField (
				new Rect (rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight),
				element, new GUIContent("Event Screen"));
	
		};*/
    }

    private void OnGUI() {

        

        currentProperty = serializedObject.FindProperty("eventScreens");
        
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));

        eventScreenList.DoLayoutList();
        
        //DrawSidebar(currentProperty);

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if(eventScreenList.index > -1) {
            DrawSelectedPropertiesPanel();
        } else {
            EditorGUILayout.LabelField("Select an Event Screen from the left.");
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();
    }

    void DrawSelectedPropertiesPanel() {

        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 18,
            alignment = TextAnchor.MiddleLeft
        };

        currentProperty = eventScreenList.serializedProperty.GetArrayElementAtIndex (eventScreenList.index);

        
        GUILayout.Label("Screen", headerStyle);

        DrawField("screenID", true);
        DrawField("text", true);
        
        EditorGUILayout.BeginHorizontal();
            DrawField("eventImage", true);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Options", headerStyle);

        var optionListElement = currentProperty.FindPropertyRelative("optionList");
        string listKey = currentProperty.propertyPath;

        ReorderableList optionList;
        
        if(optionListDict.ContainsKey(listKey)) {
            optionList = optionListDict[listKey];
        } else {
            optionList = new ReorderableList (serializedObject,
                currentProperty.FindPropertyRelative ("optionList"),
                true, true, true, true);

            optionList.drawHeaderCallback = rect => {
                EditorGUI.LabelField(rect, "Event Screens");
            };

            optionList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = optionList.serializedProperty.GetArrayElementAtIndex (index);
			    rect.y += 2; 
                EditorGUI.PropertyField(
                new Rect( rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("prompt"));
            };

            optionListDict[listKey] = optionList;
        }
        optionList.DoLayoutList();

        if(optionList != null && optionList.index > -1) {
            GUILayout.Label("Effects", headerStyle);
            SerializedProperty option = optionList.serializedProperty.GetArrayElementAtIndex (optionList.index);
            
            SerializedProperty effects = option.FindPropertyRelative("optionEffects");
            
            //EditorGUILayout.PropertyField(effects, true);

            if(effects != null) {

                Dictionary<string, int> screenListNames = new Dictionary<string, int>();
                int count = 0;
                foreach(SerializedProperty p in eventScreenList.serializedProperty) {
                    screenListNames.Add(p.FindPropertyRelative("screenID").stringValue, count);
                    count++;
                }

                for(int i = 0; i < effects.arraySize; i++) {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    
                    SerializedProperty effect = effects.GetArrayElementAtIndex(i);
                    SerializedProperty type = effect.FindPropertyRelative("type");
                    
                    EditorGUILayout.PropertyField(type, true);
                    
                    switch((int) type.enumValueIndex) {
                        default:
                        case 0:
                            break;
                        case 1:
                            EditorGUILayout.BeginVertical();
                            EditorGUILayout.PropertyField(effect.FindPropertyRelative("newScreenID"), true);
                            int index = -1;
                            if(screenListNames.TryGetValue(effect.FindPropertyRelative("newScreenID").stringValue, out index)) {
                                if(GUILayout.Button("Goto")) {
                                    eventScreenList.index = index;
                                    return;
                                }
                            } else {
                                EditorGUILayout.HelpBox("No event screen associated with this ID!", MessageType.Warning);
                            }
                            EditorGUILayout.EndVertical();
                            break;
                        case 2:
                            EditorGUILayout.PropertyField(effect.FindPropertyRelative("mod"), true);
                            break;
                        case 3:
                            EditorGUILayout.PropertyField(effect.FindPropertyRelative("mod"), true);
                            break;
                        case 4:
                            EditorGUILayout.PropertyField(effect.FindPropertyRelative("mod"), true);
                            break;
                        case 5:
                            EditorGUILayout.PropertyField(effect.FindPropertyRelative("mod"), true);
                            EditorGUILayout.PropertyField(effect.FindPropertyRelative("memory"), true);
                            break;
                    }
                    EditorGUILayout.EndVertical();

                    if(GUILayout.Button("Delete Effect")) {
                        if(effects.arraySize > 0) {
                            effects.DeleteArrayElementAtIndex(i);
                        }
                    }

                    EditorGUILayout.EndHorizontal();

                }
            }

            if(GUILayout.Button("Add New Effect")) {
                if(effects.arraySize > 0) {
                    effects.InsertArrayElementAtIndex(optionList.index);
                } else {
                    effects.arraySize++;
                }
            }
            

        }

        GUILayout.Label("Preview", headerStyle);
        Texture2D tex = AssetPreview.GetAssetPreview(currentProperty.FindPropertyRelative("eventImage").objectReferenceValue);
        if(tex != null) { GUILayout.Label(tex); }

    }


}
