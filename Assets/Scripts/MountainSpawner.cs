using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainSpawner : MonoBehaviour
{
    [SerializeField] Mountain mountain;
    [SerializeField] float scrollSpeed = -2f;
    [SerializeField] Vector3 spawnPosition = new Vector3(28.92f, 0.99f, 0);
    [SerializeField] Vector3 firstSpawnPosition; 
    // Start is called before the first frame update
    void Start()
    {
        var newMountain = Instantiate(mountain, firstSpawnPosition, Quaternion.identity);
        newMountain.SetSpeed(scrollSpeed);
        newMountain.SetMountainSpawner(gameObject.GetComponent<MountainSpawner>());
    }

    public void SetSpawnPosition(Vector3 newSpawnPosition)
    {
        spawnPosition = newSpawnPosition;
    }
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
    public void Spawn()
    {
        var newMountain = Instantiate(mountain, spawnPosition , Quaternion.identity);
        newMountain.SetSpeed(scrollSpeed);
        newMountain.SetMountainSpawner(gameObject.GetComponent<MountainSpawner>());
    }
}
