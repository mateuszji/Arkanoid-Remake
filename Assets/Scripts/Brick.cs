using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitsToDestroy;
    private string color;
    private SpriteRenderer sr;

    private void Start()
    {
        if (!sr) sr = GetComponent<SpriteRenderer>();

        color = BricksManager.Instance.generateColor();
        sr.sprite = BricksManager.Instance.getSprite(color, hitsToDestroy);
        Debug.Log(sr.sprite.bounds);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ball")
            HitBrick();
    }

    private void HitBrick()
    {
        hitsToDestroy--;
        AudioManager.Instance.breakBrick();

        if (hitsToDestroy <= 0)
        {
            Destroy(gameObject);
        }
        else
            sr.sprite = BricksManager.Instance.getSprite(color, hitsToDestroy);


    }
}
