using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.isPressed)
        {
            animator.SetBool("open", true);
        }
    }
}
