using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    // State and Time
    string state = "title";
    float timer = 0;
    [SerializeField] Bird birdPrefab;
    bool fastScrollPowerup;
    
    // Scroll Speed
    [Header("Scroll Speed Settings")]
    [SerializeField] float startingScrollSpeed = 10f;
    [SerializeField] float maxScrollSpeed = 20f;
    [SerializeField] float scrollSpeedIncreaseRate = 1f;
    [SerializeField] float scrollSpeed;
    float realScrollSpeedIncreaseRate;

    // Score
    int score = 0;
    float scoreTimeIncrement = 0.2f;

    // Cached Variables
    AudioPlayer audioPlayer;
    Bird currentBird;
    HighScoreText highScoreText;
    FadeText[] fadeTexts;
    Spawner[] spawners;
    WindPlayer windPlayer;
    MountainSpawner groundSpawner;
    AudioSource[] audioSources;
    private void Start()
    {
        fadeTexts = FindObjectsOfType<FadeText>();
        highScoreText = FindObjectOfType<HighScoreText>();
        spawners = FindObjectsOfType<Spawner>();
        currentBird = FindObjectOfType<Bird>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        groundSpawner = GameObject.Find("Ground Spawner").GetComponent<MountainSpawner>();
        if (!currentBird)
        {
            currentBird = Instantiate(birdPrefab);
        }

        windPlayer = FindObjectOfType<WindPlayer>();

        realScrollSpeedIncreaseRate = -scrollSpeedIncreaseRate / 1000;
        scrollSpeed = -startingScrollSpeed;
    }
    private void Update()
    {
        if (state == "start")
        {
            if (!fastScrollPowerup)
            {
                scrollSpeed = Mathf.Max(-maxScrollSpeed, scrollSpeed + realScrollSpeedIncreaseRate);
            }
            if (Time.time - timer > scoreTimeIncrement)
            {
                score += Mathf.RoundToInt(-scrollSpeed/10);
                timer = Time.time;
            }
            if (Input.GetKeyDown(KeyCode.P)){
                audioSources = FindObjectsOfType<AudioSource>();
                Time.timeScale = 0f;
                state = "pause";
                foreach(AudioSource audioSource in audioSources)
                {
                    audioSource.Pause();
                }

            }
        }

        else if (state == "title")
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                StartGame();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
            
        }
        else if (state == "pause")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Time.timeScale = 1f;
                state = "start";
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.UnPause();
                }
            }
        }
    }

    public void StartSpawners()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.StartSpawning();
        }
    }

    public void StopSpawners()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }

    public void StartGame()
    {
        state = "start";
        StartSpawners();
        FadeOutStartText();
        score = 0;
        timer = Time.time;
    }

    public void StartFastScrollPowerUp()
    {
        if (!fastScrollPowerup)
        {
            StartCoroutine(FastScrollPowerUp());
        }
    }

    IEnumerator FastScrollPowerUp()
    {

        float cachedScrollSpeed = scrollSpeed;
        scrollSpeed = -100f;

        fastScrollPowerup = true;
        currentBird.SetInvulnerable();

        // Make flying particles from bird longer
        ParticleSystem.MainModule particleSystem = currentBird.transform.Find("Flying Particles").GetComponent<ParticleSystem>().main;
        float cachedMultiplier = particleSystem.startLifetimeMultiplier;
        particleSystem.startLifetimeMultiplier = 1.5f;

        // Make wind sound louder and higher pitched
        AudioSource windAudio = windPlayer.GetComponent<AudioSource>();
        float cachedVolume = windAudio.volume;
        float cachedPitch = windAudio.pitch;
        int cachedPriority = windAudio.priority;
        windAudio.volume = 1f;
        windAudio.pitch = 1.5f;
        windAudio.priority = 0;

        // Make ground spawner spawn differently so don't have gaps in ground
        Vector3 cachedSpawnPosition = groundSpawner.GetSpawnPosition();
        groundSpawner.SetSpawnPosition(new Vector3(cachedSpawnPosition.x - 3.17f, cachedSpawnPosition.y, cachedSpawnPosition.z));
        yield return new WaitForSeconds(5f);

        // Return all values to original values
        particleSystem.startLifetimeMultiplier = cachedMultiplier;

        fastScrollPowerup = false;

        scrollSpeed = cachedScrollSpeed;

        windAudio.priority = cachedPriority;
        windAudio.volume = cachedVolume;
        windAudio.pitch = cachedPitch;
        currentBird.SetVulnerable();

        groundSpawner.SetSpawnPosition(cachedSpawnPosition);

    }
    public void EndGame()
    {
        StopSpawners();
        FadeInStartText();
        DestroySpawns();
        state = "title";
        scrollSpeed = -startingScrollSpeed;
        highScoreText.UpdateHighScore(score);
        StartCoroutine(SpawnNewBird());
    }

    IEnumerator SpawnNewBird()
    {
        yield return new WaitUntil(() => FindObjectOfType<Bird>() == null);
        currentBird = Instantiate(birdPrefab);
    }
    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }
    private void FadeOutStartText()
    {
        foreach (FadeText fadeText in fadeTexts)
        {
            fadeText.FadeOut();
        }
    }

    private void FadeInStartText()
    {
        foreach (FadeText fadeText in fadeTexts)
        {
            fadeText.FadeIn();
        }
    }

    private void DestroySpawns()
    {
        var spawns = FindObjectsOfType<Spawnable>();
        foreach (Spawnable spawn in spawns) {
            spawn.GetComponent<FadeObject>().TriggerFadeOut();
        }
    }

    public int GetScore()
    {
        return score;
    }

    

}
