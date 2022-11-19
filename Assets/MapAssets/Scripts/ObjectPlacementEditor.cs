using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectPlacement))]
public class ObjectPlacementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectPlacement script = (ObjectPlacement)target;
        if(GUILayout.Button("Generate"))
        {
            script.Generate();
        }
        if(GUILayout.Button("Clear"))
        {
            script.Clear();
        }
    }
}
