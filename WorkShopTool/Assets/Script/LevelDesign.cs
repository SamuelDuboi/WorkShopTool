using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewLevelDesign")]
public class LevelDesign : ScriptableObject
{
    public int width = 11;
    public int height = 20;
    [SerializeField]  public List< TileTypeIndex> tiles;
    public TileTypeInspector reftile;
     

    public void ActualiseList()
    {
         List<TileTypeIndex> _list =  new List<TileTypeIndex>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                _list.Add(new TileTypeIndex(reftile.tileTypes));
                if (tiles[x + width * y].index < reftile.tileTypes.Count)
                {
                    _list[x + width * y].index = tiles[x + width * y].index;
                }
                else
                {
                    _list[x + width * y].index = 0;
                }
            }
        }
        tiles = _list;

    }


    public void Init()
    {
        Debug.Log("yo");

        tiles = new List<TileTypeIndex>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                tiles.Add( new TileTypeIndex(reftile.tileTypes));
            }            
        }
    }
}

