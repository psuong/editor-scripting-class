﻿using System.Collections;
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
        enemyInfoProperty = serializedObject.FindProperty("enemyInfo");
    }

    // Use OnDisable() as a destructor
    private void OnDisable() {
        
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector(); // Draw the default inspector
        serializedObject.Update();  // Update the target object's state

        EditorGUI.BeginChangeCheck(); // Start checking whether or not the Editor has been changed

        // Our list will go here.
        
        // If the inspector's values change...
        if (EditorGUI.EndChangeCheck()) {
            // Apply the changes with undo support!
            serializedObject.ApplyModifiedProperties(); 
        }
    }
}
