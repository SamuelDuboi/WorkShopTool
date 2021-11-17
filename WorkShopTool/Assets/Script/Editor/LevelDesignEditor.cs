using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(LevelDesign))]
public class LevelDesignEditor : Editor
{
    public LevelDesign levelTarget;

    private int width;
    private int height;
    private int MenuType = 0;
    private int actualType = 0;
    private bool[] buttons;
    private GUIStyle buttonStyle;
    private GUIStyle boxColor;
    private GUIStyle labelStyle;
    private bool doOnce;
    private SerializedProperty test;
    private void OnEnable()
    {
        levelTarget = target as LevelDesign;
        width = levelTarget.width;

        height = levelTarget.height;
        if (levelTarget.reftile)
        {
            buttons = new bool[levelTarget.reftile.tileTypes.Count];

            if (levelTarget.tiles == null || levelTarget.tiles.Count == 0)
                levelTarget.Init();
            else
                levelTarget.ActualiseList();
        }

       
    }
    #region cour
    /*    private void TestOpti()
        {
            EditorGUI.BeginChangeCheck();
            {
                //do stuff when press on inspector
                if (EditorGUI.EndChangeCheck())
                {
                    //do stuff when relase 
                    Undo.RecordObject(stufftosave, "name of save");
                }
            }
        }*/
    /*    private void GetChidl()
        {
            SerializedProperty _struct = serializedObject.FindProperty("Struct");
            SerializedProperty subSp = _struct.FindPropertyRelative("Color");

        }
        private void TestArray()
        {
            SerializedProperty collorArray = serializedObject.FindProperty("Color");
            SerializedProperty secondColor;
            EditorGUILayout.PropertyField(collorArray);
            secondColor = collorArray.GetArrayElementAtIndex(1);
            collorArray.InsertArrayElementAtIndex(1);
            collorArray.MoveArrayElement(1, 0);
            collorArray.DeleteArrayElementAtIndex(1);
        }
            private void TestIterator()
        {
            SerializedProperty it = serializedObject.GetIterator();
            while (it.Next(true))
                Debug.Log(it.name);
            SerializedObject so = new SerializedObject(it.objectReferenceValue);
            SerializedProperty sp = so.FindProperty("m_Position");
            sp.vector3IntValue += Vector3Int.down * 10;
        }*/
    #endregion
    public override void OnInspectorGUI()
    {

        if (!doOnce)
        {
            doOnce = true;
            buttonStyle = new GUIStyle(EditorStyles.miniButton);
            boxColor = new GUIStyle(EditorStyles.helpBox);
            labelStyle = new GUIStyle(EditorStyles.label);
            labelStyle.fontSize += 10;
            buttonStyle.border = new RectOffset(0, 0, 0, 0);
            buttonStyle.contentOffset = Vector2.zero;
            buttonStyle.fixedHeight = 32;
            buttonStyle.padding = new RectOffset(0, 0, 0, 0);
            buttonStyle.margin = new RectOffset(0, 0, 0, 0);
            buttonStyle.active.background = MakeTex((Screen.width - 30) / buttons.Length, 16, Color.gray);
        }

        if (!levelTarget.reftile)
        {
            levelTarget.reftile = (TileTypeInspector)EditorGUILayout.ObjectField(levelTarget.reftile, typeof(TileTypeInspector), true);
            if (levelTarget.reftile)
            {
                buttons = new bool[levelTarget.reftile.tileTypes.Count];

                if (levelTarget.tiles == null)
                    levelTarget.Init();
                else
                    levelTarget.ActualiseList();
            }
            Repaint();
            EditorUtility.SetDirty(levelTarget);
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            return;
        }


        DrawPresset();
        EditorGUILayout.BeginVertical(boxColor, GUILayout.MaxWidth(100));
            DrawTypeButtons();
            EditorGUILayout.Space(15);

            DrawLd();
            EditorGUILayout.Space(10);

            EditorGUILayout.EndVertical();

        EditorGUILayout.Space(30);

       

        Repaint();
        EditorUtility.SetDirty(levelTarget);
        serializedObject.ApplyModifiedProperties(); 
        serializedObject.Update();

    }

    public void DrawPresset()
    {
        EditorGUILayout.LabelField("Presets", labelStyle);
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(DrawWallTexture(), buttonStyle, GUILayout.Width(60), GUILayout.Height(60)))
        {
            for (float y = 0; y < height; y++)
            {

                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < width; x++)
                {
                    if (y < height / 6 || y > height * 5 / 6)
                    {
                        levelTarget.tiles[x + width * (int)y].index = 1;
                    }
                    else
                    {
                        levelTarget.tiles[x + width * (int)y].index = 0;
                    }

                }
                EditorGUILayout.EndHorizontal();

            }
        }
        if (GUILayout.Button(DrawSinWallTexture(), buttonStyle, GUILayout.Width(60), GUILayout.Height(60)))
        {
          
            for (float y = 0; y < height; y++)
            {
                
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < width; x++)
                {
                    var value = Mathf.Sin(x * Mathf.PI / width) * height * 4 / 10 - 1;
                    if (y< value+1 || y > height - value -1||y ==0 ||y ==height-1)
                    {
                        levelTarget.tiles[x + width *(int) y].index = 1;
                    }
                    else
                    {
                        levelTarget.tiles[x + width * (int)y].index = 0;
                    }

                }
                EditorGUILayout.EndHorizontal();

            }
        }
        if (GUILayout.Button(DrawAliasingWallTexture(), buttonStyle, GUILayout.Width(60), GUILayout.Height(60)))
        {
            float substractValue = 2;
            float value = 3;
            for (float y = 0; y < height; y++)
            {

                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < width; x++)
                {

                    if (y % 3 == 0)
                        substractValue *= -1;
                    Debug.Log(value - substractValue +"y" +y.ToString());
                    if (y < value + 1 || y > height - value - 1 || y == 0 || y == height - 1)
                    {
                        levelTarget.tiles[x + width * (int)y].index = 1;
                    }
                    else
                    {
                        levelTarget.tiles[x + width * (int)y].index = 0;
                    }

                }
                EditorGUILayout.EndHorizontal();

            }
        }
            EditorGUILayout.EndHorizontal();

    }
    public Texture2D DrawWallTexture( )
    {
        Color[] pix = new Color[60 * 60];
        for (int y = 0; y < 60; y++)
        {
            for (int x = 0; x < 60; ++x)
            {
                if(y<10 || y>50)
                pix[x+ 60*y] = levelTarget.reftile.tileTypes[1].colors;
                else
                    pix[x + 60 * y] = Color.gray;
            }
        }
        Texture2D result = new Texture2D(60, 60);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    public Texture2D DrawSinWallTexture()
    {
        Color[] pix = new Color[60 * 60];
        float value = 0;
        for (int y = 0; y < 60; y++)
        {
           
            for (int x = 0; x < 60; ++x)
            {
                value = Mathf.Sin(x * Mathf.PI / 60) * 60 * 3 / 10;
                
                if (y < value || y > 60-value)
                    pix[x + 60 * y] = levelTarget.reftile.tileTypes[1].colors;
                else
                    pix[x + 60 * y] = Color.gray;
            }
        }
      
        Texture2D result = new Texture2D(60, 60);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    public Texture2D DrawAliasingWallTexture()
    {
        Color[] pix = new Color[60 * 60];
        float substractValue = 5;
        float value = 6;
        for (int x = 0; x < 60; x++)
        {

            for (int y = 0; y < 60; y++)
            {
                if (y % 3 == 0)
                    substractValue *= -1;
                if (y < value -substractValue || y > 60 - value +substractValue)
                    pix[x + 60 * y] = levelTarget.reftile.tileTypes[1].colors;
                else
                    pix[x + 60 * y] = Color.gray;
            }
        }

        Texture2D result = new Texture2D(60, 60);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    public Texture2D DrawDoubleWallTexture()
    {
        Color[] pix = new Color[60 * 60];
        float value = 0;
        for (int y = 0; y < 60; y++)
        {

            for (int x = 0; x < 60; ++x)
            {
                value = Mathf.Sin(x * Mathf.PI / 60) * 60 * 3 / 10;
                if (x < value || x > 60 - value)
                    pix[x + 60 * y] = levelTarget.reftile.tileTypes[1].colors;
                else
                    pix[x + 60 * y] = Color.gray;
            }
        }

        Texture2D result = new Texture2D(60, 60);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    public void DrawTypeButtons()
    {
        EditorGUILayout.BeginHorizontal();
        var tempBackground = buttonStyle.normal.background;
        for (int i = 0; i < buttons.Length; i++)
        {
            //buttons[i] = GUILayout.Button(MakeTex(32*width/buttons.Length,32, levelTarget.reftile.tileTypes[i].colors), buttonSty, GUILayout.Width(32*width / buttons.Length));

            buttonStyle.normal.background = MakeTex(32 * width / buttons.Length, 32, levelTarget.reftile.tileTypes[i].colors);
            EditorGUI.BeginChangeCheck();
            buttons[i] = GUILayout.Button(levelTarget.reftile.tileTypes[i].names, buttonStyle, GUILayout.Width(32 * width / buttons.Length));
            var changed = EditorGUI.EndChangeCheck();

            if (changed && buttons[i])
            {
                Debug.Log("changed");
                actualType = i;
                boxColor.normal.background = MakeTex(100, 100, levelTarget.reftile.tileTypes[i].colors);
                boxColor.hover.background = MakeTex(100, 100, levelTarget.reftile.tileTypes[i].colors);
            }
        }
        buttonStyle.normal.background = tempBackground;
        EditorGUILayout.EndHorizontal();
    }
    public void DrawLd()
    {
        EditorGUI.BeginChangeCheck();
        for (int y = 0; y < height; y++)
        {
            EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < width; x++)
                {
                    if (DrawEnum(x, y))
                    {
                        Undo.RegisterCompleteObjectUndo(levelTarget, "Tile Change");
                        if (levelTarget.tiles[x + width * y].index == actualType)
                        {

                            levelTarget.tiles[x + width * y].index = 0;
                            MakeTex((Screen.width - 50) / width, 32, levelTarget.tiles[x + width * y].tileTypeArray[levelTarget.tiles[x + width * y].index].colors);
                        }
                        else
                            levelTarget.tiles[x + width * y].index = actualType;

                    }
                }
            EditorGUILayout.EndHorizontal();

        }
        EditorGUI.EndChangeCheck();
    }
    public bool DrawEnum(int x, int y)
    {
        if (levelTarget.tiles.Count == 0)
            levelTarget.Init();
        return GUILayout.Button(MakeTex(32,32,levelTarget.tiles[x+width* y].tileTypeArray[levelTarget.tiles[x+width* y].index].colors), buttonStyle, GUILayout.Width(32), GUILayout.Height(24));
    }

    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
     
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
