using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    Rigidbody2D rigidBody;
    GameController gameController;
    public void SetSpeed(float newSpeed)
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(newSpeed, rigidBody.velocity.y);

    }

    public void Update()
    {
        SetSpeed(gameController.GetScrollSpeed());
    }

}
