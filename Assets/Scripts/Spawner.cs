using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject mini;
    [SerializeField] Vector2 spawnPos;

    private Animator animator;

    public bool ready = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {

        animator.SetBool("open", true);
    }

    public void Wave(int count, float speed)
    {
        StartCoroutine(Spawn(count, speed));
    }

    private IEnumerator Spawn(int count, float speed)
    {
        ready = false;

        for (int i = 0; i < count; i++)
        {
            GameObject newMini = Instantiate(mini, spawnPos, Quaternion.identity);
            newMini.GetComponent<Mini>().Spawn(transform.localScale.x > 0 ? 1 : -1);

            yield return new WaitForSeconds(1f / speed);
        }

        ready = true;
    }
}
