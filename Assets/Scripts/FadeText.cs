using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    float duration = 2f;
    TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    
    public void FadeOut()
    {
        textMeshPro.CrossFadeColor(new Color(1f, 1f, 1f, 0), duration, true, true);
    }

    public void FadeIn()
    {
        textMeshPro.CrossFadeColor(new Color(1f, 1f, 1f, 1), duration, true, true);
    }
    
}
