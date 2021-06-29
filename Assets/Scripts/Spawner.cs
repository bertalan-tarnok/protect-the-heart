using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject mini;
    [SerializeField] Vector2 spawnPos;

    private List<GameObject> minis = new List<GameObject>();

    private Animator animator;

    public bool ready = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        for (int i = 0; i < minis.Count; i++)
        {
            if (minis[i] == null)
            {
                minis.RemoveAt(i);
            }
        }
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
            newMini.transform.parent = transform;

            minis.Add(newMini);

            yield return new WaitForSeconds(1f / speed);
        }

        ready = true;
    }
}
