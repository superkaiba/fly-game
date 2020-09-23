using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    [SerializeField] float minRotationSpeed = 90f;
    [SerializeField] float maxRotationSpeed = 360f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        float newAngularVelocity = Random.Range(minRotationSpeed, maxRotationSpeed);
        if (Random.Range(1,3) == 2)
        {
            newAngularVelocity = -newAngularVelocity;
        }
        rigidbody2D.angularVelocity = newAngularVelocity;
    }
}
