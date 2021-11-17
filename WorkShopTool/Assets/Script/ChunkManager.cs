using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public LevelDesign[] LD;
    public TileTypeInspector tileInspector;
    public List< ChunckBehavior> chunks = new List<ChunckBehavior>();
    public GameObject prefab;
    public Transform m_transform;
    private LevelDesign currentLevel;
    private LevelDesign nextLevel;
    private float previousPosInt;
    private int nextLevelRow;
    public float speed;

#if UNITY_EDITOR
    public bool foldoutChunks;
    public List< int> poolSize = new List<int>();
#endif
    private void Start()
    {
        previousPosInt = m_transform.position.y;
        SendNewChunk(Vector2.up * 5, prefab);
    }
    private void Update()
    {
       // transform.Translate(Vector2.down * speed * Time.deltaTime);
        if(m_transform.position.y< previousPosInt - 1)
        {
            previousPosInt = m_transform.position.y;
            InstantiateRow();
        }
    }
    private bool InstantiateRow()
    {
        if (nextLevelRow < nextLevel.height)
        {
            //spawnThisRow
        }
        return false;
    }
    public void SendNewChunk(Vector2 pos, GameObject _prefab)
    {
        GetFreeElement(_prefab).StartMoving(pos, speed);
    }
    public void SendNewChunk()
    {
        GetFreeElement(prefab).StartMoving(Vector2.up*5, speed);
    }
    private ChunckBehavior GetFreeElement(GameObject _prefab)
    {
       /* for (int i = 0; i < chunks.Count; i++)
            if ((!chunks[i].enabled))
                return chunks[i];*/
        int safety = 100;
        for (int i = 0; i < safety; i++)
        {
            if (!chunks[Random.Range(0, chunks.Count)].enabled)
                return chunks[i];
        }
        Debug.LogWarning("No free chunk");


        return ExtendPool(_prefab);
    }
    private ChunckBehavior ExtendPool(GameObject _prefab)
    {
        GameObject go = Instantiate(_prefab, m_transform);
        ChunckBehavior cb = go.GetComponent<ChunckBehavior>();
        if (!cb) cb = go.AddComponent<ChunckBehavior>();
        return cb;
    }
}
