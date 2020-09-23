using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVerticalMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float maxYVelocity = 2f;
    [SerializeField] float minYVelocity = -2f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, Random.Range(minYVelocity, maxYVelocity));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
