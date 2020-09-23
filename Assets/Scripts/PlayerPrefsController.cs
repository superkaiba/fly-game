using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_HIGHSCORE_KEY = "highscore";

    public static void SetHighScore(int highscore)
    {
        PlayerPrefs.SetInt(MASTER_HIGHSCORE_KEY, highscore);
    }

    public static int GetHighScore()
    { 
        return PlayerPrefs.GetInt(MASTER_HIGHSCORE_KEY, 0);
    }
}
