using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] float minScale;
    [SerializeField] float maxScale;

  
   
    public float GetScale()
    {
        return Random.Range(minScale, maxScale);
    }

    public float GetY()
    {
        return Random.Range(minY, maxY);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<SpawnableDestroyer>())
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}
