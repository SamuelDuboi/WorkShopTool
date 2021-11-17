using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="TileInspector")]
[System.Serializable]
public class TileTypeInspector : ScriptableObject
{
    public List< TileType> tileTypes ;

    public void AddNewTiles()
    {
        if (tileTypes == null)

        {
            tileTypes = new List<TileType>();
        }
        tileTypes.Add(new TileType("NewName", Color.black,null));
    }
}
[System.Serializable]
public class TileType
{
    public TileType(string _name, Color _color, GameObject _prefab)
    {

        names = _name;
        colors = _color;
        prefab = _prefab;
    }
    public string names;
    public GameObject prefab;
    public Color colors;
}
[System.Serializable]
public class TileTypeIndex
{


    [SerializeField] public int index;
    [SerializeField] public TileType[] tileTypeArray ;
    public TileTypeIndex(List<TileType> _tileTypeArray)
    {
        index = 0;
        tileTypeArray = _tileTypeArray.ToArray();
    }
}
[System.Serializable]
public struct TestStruct
{

}