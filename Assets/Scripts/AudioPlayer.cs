using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AnimalCall[] animalCalls;
    
    [SerializeField] float minDurationBetweenSounds = 2f;
    [SerializeField] float maxDurationBetweenSounds = 10f;

    bool playSounds = true;
    int animalCallsLength;

    // Start is called before the first frame update
    void Start()
    {
        animalCallsLength = animalCalls.Length;
        StartCoroutine(PlayRandomSounds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayRandomSounds()
    {
        while (playSounds)
        {
            yield return new WaitForSeconds(Random.Range(minDurationBetweenSounds, maxDurationBetweenSounds));
            AnimalCall animalCall = animalCalls[Random.Range(0, animalCallsLength)];
            AudioSource.PlayClipAtPoint(animalCall.AudioClip, Camera.main.transform.position, animalCall.Volume);
            Debug.Log("Played sound");
        }
    }
}
