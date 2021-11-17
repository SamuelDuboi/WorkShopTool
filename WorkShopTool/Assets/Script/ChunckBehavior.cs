using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunckBehavior : MonoBehaviour
{
    public Transform m_transform;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;   
    }
    public void StartMoving(Vector2 pos, float _speed)
    {
        enabled = true;
        speed = _speed;
        m_transform.position = pos;
    }

    public void Stop()
    {
        enabled = false;
        speed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        m_transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
