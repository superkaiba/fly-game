using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gradient : MonoBehaviour
{
    [SerializeField] Color[] colors;
    [SerializeField] float blendTimeMin = 10f;
    [SerializeField] float blendTimeMax = 20f;

    [SerializeField] Color firstColor;
    [SerializeField] Color secondColor;
    [SerializeField] Color thirdColor;
    [SerializeField] Color fourthColor;
    float timer = 0;
    float blendTime;

    public UnityEngine.UI.RawImage img;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    private Texture2D backgroundTexture;
    void Awake()
    {
        backgroundTexture = new Texture2D(1, 2);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;

        firstColor = colors[Random.Range(0, colors.Length)];
        thirdColor = colors[Random.Range(0, colors.Length)];

        secondColor = colors[Random.Range(0, colors.Length)];
        fourthColor = colors[Random.Range(0, colors.Length)];

        SetColor(firstColor, thirdColor);
        blendTime = Random.Range(blendTimeMin, blendTimeMax);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float timeProportion = timer / blendTime;

        if(timeProportion >= 1)
        {
            SetNewColors();
            timer = 0;
            timeProportion = 0;
        }

        color1 = Color.Lerp(firstColor, secondColor, timeProportion);
        color2 = Color.Lerp(thirdColor, fourthColor, timeProportion);

        SetColor(color1, color2);
    }

    private void SetNewColors()
    {
        firstColor = secondColor;
        thirdColor = fourthColor;

        secondColor = colors[Random.Range(0, colors.Length)];
        fourthColor = colors[Random.Range(0, colors.Length)];
    }
    public void SetColor(Color color1, Color color2)
    {
        backgroundTexture.SetPixels(new Color[] { color1, color2 });
        backgroundTexture.Apply();
        img.texture = backgroundTexture;
    }
}
