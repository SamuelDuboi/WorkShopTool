using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CustomEditor(typeof(TileTypeInspector))]
public class TileTypeEditor : Editor
{
    private TileTypeInspector tileTraget;
    private List<bool> foldout;
    private void OnEnable()
    {
        tileTraget = target as TileTypeInspector;
        foldout = new List<bool>();

        if (tileTraget.tileTypes == null)
            tileTraget.tileTypes = new List<TileType>();
        for (int i = 0; i < tileTraget.tileTypes.Count; i++)
        {
            foldout.Add(false);
        }

    }

    public override void OnInspectorGUI()
    {
        for (int i = 0; i < foldout.Count; i++)
        {

            foldout[i] = EditorGUILayout.Foldout(foldout[i], tileTraget.tileTypes[i].names);
            if (foldout[i])
            {
                tileTraget.tileTypes[i].names = EditorGUILayout.TextField("Name", tileTraget.tileTypes[i].names);
                tileTraget.tileTypes[i].colors = EditorGUILayout.ColorField("Color", tileTraget.tileTypes[i].colors);
                tileTraget.tileTypes[i].prefab = (GameObject) EditorGUILayout.ObjectField("Prefab", tileTraget.tileTypes[i].prefab, typeof(GameObject),true);
            }
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            tileTraget.AddNewTiles();
            foldout.Add(false);
        }
        if(tileTraget.tileTypes.Count>2)
            if (GUILayout.Button("-"))
            {
                tileTraget.tileTypes.RemoveAt(tileTraget.tileTypes.Count - 1);
                foldout.RemoveAt(foldout.Count - 1);
            }
                EditorGUILayout.EndHorizontal();



        Repaint();
        EditorUtility.SetDirty(tileTraget);
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

  
}
