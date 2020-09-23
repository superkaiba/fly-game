using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthText : MonoBehaviour
{
    [SerializeField] Color healthyColor;
    [SerializeField] Color deadColor;
    Bird bird;

    [SerializeField] HealthDisplaySprite[] healthDisplaySprites;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateHealthDisplay(int currentHealth)
    {
        bird = FindObjectOfType<Bird>();
        for (int i = 0; i < bird.GetMaxHealth(); i++)
        {
            if (i < currentHealth)
            {
                Image image = healthDisplaySprites[i].GetComponent<Image>();
                image.color = healthyColor;
            }
            else
            {
                Image image = healthDisplaySprites[i].GetComponent<Image>();
                image.color = deadColor;
            }
        }
    }
    
}
