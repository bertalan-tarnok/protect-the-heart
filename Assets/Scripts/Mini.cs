using System.Collections;
using UnityEngine;

public class Mini : MonoBehaviour
{
    private float speed = 1.5f;

    private Animator animator;
    private bool isKilling = false;

    private Heart heart;

    private void Start()
    {
        animator = GetComponent<Animator>();
        heart = FindObjectOfType<Heart>();
    }

    public void Spawn(int direction)
    {
        StartCoroutine(Go(direction));
    }

    private IEnumerator Go(int direction)
    {
        LeanTween.moveY(gameObject, transform.position.y + 0.7f, 0.2f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                LeanTween.moveY(gameObject, 0f, 0.1f).setEase(LeanTweenType.easeInQuad);
            });

        LeanTween.moveX(gameObject, transform.position.x + 0.4f * direction, 0.3f);

        yield return new WaitForSeconds(0.32f);

        while (transform.position.x * direction < 0)
        {
            transform.position += Vector3.right * speed * direction * Time.deltaTime;

            if (Mathf.Abs(transform.position.x) < 1 && !isKilling)
            {
                animator.SetTrigger("Kill");
                speed = 2.5f;
                isKilling = true;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void Kill()
    {
        heart.Die();
    }
}
