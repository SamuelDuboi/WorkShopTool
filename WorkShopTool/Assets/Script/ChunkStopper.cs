using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStopper : MonoBehaviour
{
    public ChunkManager chunkManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ChunckBehavior>().Stop();
        chunkManager.SendNewChunk();
    }
}
