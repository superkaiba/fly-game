using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip[] pickupSounds;
    [SerializeField] float pickupVolume = 1f;
    Spawner spawner;
    float timeSinceSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceSpawn = 0;
    }

    public void SetSpawner(Spawner currentSpawner)
    {
        spawner = currentSpawner;
    }
    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (timeSinceSpawn < 0.7)
        {
            if (otherCollider.GetComponent<Spawnable>())
            {
                Debug.Log("Destroy Rose");
                spawner.Spawn();
                Destroy(gameObject);
            }
        }
    }
    public void TriggerPickup()
    {
        if (pickupSounds.Length != 0)
        {
            AudioSource.PlayClipAtPoint(pickupSounds[Random.Range(0, pickupSounds.Length)], Camera.main.transform.position, pickupVolume);
        }
        Destroy(gameObject);
        
    }
}
