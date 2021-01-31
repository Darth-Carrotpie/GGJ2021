using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MobMovement : MonoBehaviour
{
    public float wanderRadius = 10;
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
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
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
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
    {
        /*Vector3 randDirection = Random.insideUnitCircle * dist;

        randDirection += origin;

        NavMeshHit navHit;

        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }
        return Vector3.zero;*/

        NavMeshHit  navHit;
        bool        canMove = false;

        Vector3 randDirection = Random.insideUnitCircle * dist;

        randDirection += origin;

        if (!NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            while (!canMove)
            {
                randDirection = Random.insideUnitCircle * dist;

                randDirection += origin;

                canMove = NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
            }
        }

        return navHit.position;
    }
}
