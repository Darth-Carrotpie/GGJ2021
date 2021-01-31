using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MobAnimator : MonoBehaviour
{
    Animator animator;
    public float dieDuration = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        animator.SetBool("walking", true);
    }

    public void Stop()
    {
        animator.SetBool("walking", false);
    }

    public void Die()
    {
        transform.SetParent(null);
        animator.SetBool("dead", true);
        StartCoroutine(DieDie());
    }

    IEnumerator DieDie()
    {
        yield return new WaitForSeconds(dieDuration);

        Destroy(gameObject);
    }
}
