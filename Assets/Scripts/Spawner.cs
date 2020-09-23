using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Spawnable[] spawnedObjects;
    [SerializeField] float spawnedObjectspeed= 3f;
    [SerializeField] float minSpawnDifference = 1f;
    [SerializeField] float maxSpawnDifference= 5f;

    Coroutine spawningCoroutine;
    bool spawn = true;

    public void StopSpawning()
    {
        StopCoroutine(spawningCoroutine);
    }
    public void StartSpawning()
    {
        spawningCoroutine = StartCoroutine(SpawnAll());
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spawnedObjects.Length);
    }
    
    IEnumerator SpawnAll()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDifference, maxSpawnDifference));
            Spawn();
 

        }
    }

    public void Spawn()
    {
        var prefabToSpawn = spawnedObjects[Random.Range(0, spawnedObjects.Length)];
        var newSpawnY = prefabToSpawn.GetY();
        var newSpawnScale = prefabToSpawn.GetScale();
        var newPos = new Vector3(25, newSpawnY, 0);
        var newSpawn = Instantiate(prefabToSpawn, newPos, Quaternion.identity);

        if (newSpawn.GetComponent<Pickup>())
        {
            newSpawn.GetComponent<Pickup>().SetSpawner(this);
        }
        var newScaleVector = new Vector3(newSpawnScale, newSpawnScale, 1);
        newSpawn.GetComponent<Transform>().localScale = newScaleVector;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
