using System.Collections;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        GameManager.End();
    }
}
