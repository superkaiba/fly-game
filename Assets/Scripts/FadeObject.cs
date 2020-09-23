using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{

    bool fadingOut = false;
    bool fadingIn = false;

    float fadeTime = 1f;
    [SerializeField] float fadeInTime = 2f;
    float fadeTimer = 0;
    SpriteRenderer spriteRenderer;
    Color startingColor;
    Color endColor;

    public void TriggerFadeOut()
    {
        fadeTimer = 0;
        fadingOut = true;
        if (!GetComponent<Bird>()){
            Destroy(GetComponent<Collider2D>());
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer)
        {
            spriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
        }
        startingColor = spriteRenderer.color;
        endColor = new Color(startingColor.r, startingColor.g, startingColor.b, 0);
    }

    

    private void FadeOut()
    {

        fadeTimer += Time.deltaTime;
        float fadeProportion = fadeTimer / fadeTime;
        spriteRenderer.color = Color.Lerp(startingColor, endColor, fadeProportion);

        if (spriteRenderer.color.a == 0)
        {
            Destroy(gameObject);
        }
    }

    public void TriggerFadeIn()
    {
        fadingIn = true;
        fadeTimer = 0;
    }
    private void FadeIn()
    {
        fadeTimer += Time.deltaTime;
        float fadeProportion = fadeTimer / fadeInTime;
        spriteRenderer.color = Color.Lerp(endColor, startingColor, fadeProportion);
    }
    // Update is called once per frame
    void Update()
    {
        if (fadingOut)
        {
            FadeOut();
        }
        else if(fadingIn){
            FadeIn();
        }
    }
}
