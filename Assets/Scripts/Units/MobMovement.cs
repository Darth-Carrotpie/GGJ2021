using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MobMovement : MonoBehaviour
{
    public float wanderRadius = 5;
    private float timer;
    private float movementDuration = 5.0f;
    private float waitBeforeMoving = 1.0f;
    private bool hasArrived = false;
    public UnityEvent onWalk;
    public UnityEvent onStop;

    private void Update()
    {
        if (!hasArrived)
        {
            hasArrived = true;
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
            StartCoroutine(MoveToPoint(newPos));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;
        onWalk.Invoke();

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            //float eased = Easing.Cubic.InOut(t);
            float eased = Easing.Quadratic.InOut(t);
            transform.position = Vector3.Lerp(startPos, targetPos, eased);

            yield return null;
        }

        onStop.Invoke();
        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        NavMeshHit navHit;

        Vector3 randDirection = Random.insideUnitCircle * dist;

        randDirection += origin;

        for (int i = 0; i < 30; i++)
        {
            randDirection = Random.insideUnitCircle * dist;

            randDirection += origin;

            if (NavMesh.SamplePosition(randDirection, out navHit, 1.0f, NavMesh.AllAreas))
            {
                return navHit.position;
            }
        }

        return origin;
    }
}
