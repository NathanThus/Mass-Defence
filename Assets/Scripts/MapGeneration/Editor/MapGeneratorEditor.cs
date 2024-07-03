using UnityEngine;
using UnityEditor;
using NathanThus.MassDefence.MapGeneration;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Update Map"))
        {
            (target as MapGenerator).UpdatePlates();
        }

        if(GUILayout.Button("Clear Map"))
        {
            (target as MapGenerator).ClearPlates();
        }

        GUILayout.EndHorizontal();
    }
}