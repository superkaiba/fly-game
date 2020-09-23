using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    MountainSpawner mountainSpawner;
    // Start is called before the first frame update
    public void SetMountainSpawner(MountainSpawner newMountainSpawner)
    {
        mountainSpawner = newMountainSpawner;
    }
    public void SetSpeed(float newSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(newSpeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    { 
        if (otherCollider.GetComponent<EnterScreenCollider>()){
            mountainSpawner.Spawn();
        }
        else if (otherCollider.GetComponent<SpawnableDestroyer>())
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
