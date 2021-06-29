using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;

    private AudioSource deathSound;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        deathSound = GameObject.Find("Death").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
        StartCoroutine(Cooldown());
        Money.money += 1;
        deathSound.Play();
    }

    private IEnumerator Cooldown()
    {
        sprite.enabled = boxCollider.enabled = false;
        yield return new WaitForSeconds(1.2f);
        sprite.enabled = boxCollider.enabled = true;
    }
}
