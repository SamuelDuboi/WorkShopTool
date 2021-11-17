using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ChunkManager))]
public class ChunkManagerEditor : Editor
{
    SerializedProperty self;
    SerializedProperty chunkPrefab;
    SerializedProperty chunks;
    SerializedProperty foldoutChunks;
    SerializedProperty poolSize;
    SerializedProperty LD;
    SerializedProperty refTile;
    TileTypeInspector tilTypeInspector;
    LevelDesign firstLD;
    private void OnEnable()
    {
        self = serializedObject.FindProperty("m_transform");
        chunkPrefab = serializedObject.FindProperty("prefab");
        foldoutChunks = serializedObject.FindProperty("foldoutChunks");
        poolSize = serializedObject.FindProperty("poolSize");
        chunks = serializedObject.FindProperty("chunks");
        LD = serializedObject.FindProperty("LD");
        firstLD = (LevelDesign) LD.GetArrayElementAtIndex(0).objectReferenceValue;
        tilTypeInspector = firstLD.reftile;
        Undo.undoRedoPerformed += BuildPool;
    }
    private void OnDisable()
    {
        Undo.undoRedoPerformed -= BuildPool;
    }
    public override void OnInspectorGUI()
    {

       
        EditorGUILayout.LabelField("poolSize", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
       
        for (int i = 0; i < tilTypeInspector.tileTypes.Count; i++)
        {
            if (poolSize.arraySize < tilTypeInspector.tileTypes.Count)
            {
                poolSize.InsertArrayElementAtIndex(poolSize.arraySize - 1);
                poolSize.GetArrayElementAtIndex(poolSize.arraySize - 1).intValue = 0;
            }
            else if(poolSize.arraySize > tilTypeInspector.tileTypes.Count)
            {
                poolSize.DeleteArrayElementAtIndex(poolSize.arraySize - 1);
            }
            EditorGUILayout.PropertyField(poolSize.GetArrayElementAtIndex(i));
        }
        
        EditorGUILayout.BeginHorizontal();
        bool changed=  EditorGUI.EndChangeCheck();
        if (changed)
        {
            for (int i = 0; i < tilTypeInspector.tileTypes.Count; i++)
            {
                if (poolSize.GetArrayElementAtIndex(i).intValue < 0) poolSize.GetArrayElementAtIndex(i).intValue = 0;
            }
             
        }
        if (GUILayout.Button("Rebuild", EditorStyles.miniButton)) BuildPool();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        foldoutChunks.boolValue= EditorGUILayout.Foldout( foldoutChunks.boolValue, "References", true);
        if (foldoutChunks.boolValue)
        {
            EditorGUI.indentLevel += 2;
            EditorGUILayout.PropertyField(self);
            EditorGUILayout.PropertyField(chunkPrefab);
            EditorGUI.indentLevel -= 2;
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
     
    void BuildPool()
    {
        serializedObject.ApplyModifiedPropertiesWithoutUndo();

        serializedObject.Update();
        while (chunks.arraySize > 0)
        {
            SerializedProperty current = chunks.GetArrayElementAtIndex(0);
            ChunckBehavior cb = current.objectReferenceValue as ChunckBehavior;
            if (cb == null) chunks.DeleteArrayElementAtIndex(0);
            else
            {
                DestroyImmediate(cb.gameObject);
                chunks.DeleteArrayElementAtIndex(0);
            }
        }
        for (int i = 0; i < tilTypeInspector.tileTypes.Count; i++)
        {
            for (int x = 0; x < poolSize.GetArrayElementAtIndex(i).intValue; x++)
            {
                GameObject go = PrefabUtility.InstantiatePrefab(tilTypeInspector.tileTypes[i].prefab as GameObject, self.objectReferenceValue as Transform) as GameObject;
                //go.hideFlags = HideFlags.HideInHierarchy;
                Transform tr = go.transform;
                tr.position = Vector3.zero;
                tr.eulerAngles = Vector3.zero;
                tr.localScale = Vector3.one;
                EditorUtility.SetDirty(tr);
                chunks.InsertArrayElementAtIndex(chunks.arraySize);
                chunks.GetArrayElementAtIndex(chunks.arraySize - 1).objectReferenceValue = go.GetComponent<ChunckBehavior>();
            }
        }
        
        
        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }
}
