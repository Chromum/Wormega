using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AndTransition))]
public class AndTransitionEditor : Editor
{
    SerializedProperty decision;
    SerializedProperty secondDecision;
    SerializedProperty trueState;
    SerializedProperty falseState;
    
    private void OnEnable()
    {
        decision = serializedObject.FindProperty("Decision");
        secondDecision = serializedObject.FindProperty("SecondDecision");
        trueState = serializedObject.FindProperty("TrueState");
        falseState = serializedObject.FindProperty("FalseState");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(decision);
        EditorGUILayout.PropertyField(secondDecision);
        EditorGUILayout.PropertyField(trueState);
        EditorGUILayout.PropertyField(falseState);
        serializedObject.ApplyModifiedProperties();
    }
}
