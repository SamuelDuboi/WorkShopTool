using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(TestStruct))]
public class MyStructDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight + 1;
        Rect topZone = new Rect(position.x, position.y, position.width, lineHeight);
        Rect leftZone = new Rect(position.x, position.y, position.width*0.5f, lineHeight);
        Rect rightZone = new Rect(position.x+ position.x*0.5f, position.y, position.width*0.5f, lineHeight);
        Rect bottomZone = new Rect(position.x, position.y + lineHeight, position.width, lineHeight);



        EditorGUI.DrawRect(bottomZone, Color.blue);
        EditorGUI.DrawRect(leftZone, Color.red);
        EditorGUI.DrawRect(rightZone, Color.grey);
        SerializedProperty speed = property.FindPropertyRelative("Speed");

        EditorGUI.PropertyField(bottomZone, speed);

    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float numberOflIne = 2;
        float lineHeight = EditorGUIUtility.singleLineHeight;
        return lineHeight * numberOflIne;
    }
}
