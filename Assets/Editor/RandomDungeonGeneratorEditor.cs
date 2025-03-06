using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{ 
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate"))
        {
            generator.GenerateDungeon();
        }

        if (GUILayout.Button("Generate With new Seed"))
        {
            generator.GenerateDungeonNewSeed();
        }
        EditorGUILayout.EndHorizontal();
        
        if (GUILayout.Button("Clear"))
        {
            generator.ClearMap();
        }
    }
}
