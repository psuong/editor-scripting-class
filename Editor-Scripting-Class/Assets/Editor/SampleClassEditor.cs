using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // You must use this namespace to do Editor scripting - otherwise you will be using UnityEditor.Editor.
using UnityEditorInternal; // No man's land, there isn't any documentation for this publicly from Unity.

[CustomEditor(typeof(SampleClass))] // Another decorator - but this one extends functionality to target a particular class.
public class SampleClassEditor : Editor {

    private SerializedProperty enemyInfoProperty;
    private ReorderableList enemyInfoList;

    // Use the OnEnable() function as your constructor or Start() function in Unity.
    private void OnEnable() {
        // Find the script we're writing a custom inspector for
        enemyInfoProperty = serializedObject.FindProperty("enemyInfo");

        // Create the reordable list
        // It should target the "serializedObject" and some serializedProperty like enemyInfoProperty
        enemyInfoList = new ReorderableList(serializedObject, enemyInfoProperty, true, true, true, true);

        // We're going to use delegates and lambdas here!
        enemyInfoList.drawHeaderCallback += (Rect rect) => { EditorGUI.LabelField(rect, new GUIContent("Enemy Info List")); };
        enemyInfoList.drawElementCallback += OnDrawElement;
    }

    // Use OnDisable() as a destructor
    private void OnDisable() {
        enemyInfoList.drawElementCallback -= OnDrawElement;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector(); // Draw the default inspector
        serializedObject.Update();  // Update the target object's state

        EditorGUI.BeginChangeCheck(); // Start checking whether or not the Editor has been changed

        // Our list will go here
        enemyInfoList.DoLayoutList();
        
        // If the inspector's values change...
        if (EditorGUI.EndChangeCheck()) {
            // Apply the changes with undo support!
            serializedObject.ApplyModifiedProperties(); 
        }
    }

    private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty element = enemyInfoProperty.GetArrayElementAtIndex(index); // Grab the serialized element via the index

        // Use the FindPropertyRelative to get relative properties, or properties within classes and structs
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width / 2, rect.height), element.FindPropertyRelative("name"), GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2, rect.y, rect.width/2, rect.height), element.FindPropertyRelative("state"), GUIContent.none);
    }
}
