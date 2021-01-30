using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobMovement : MonoBehaviour
{
    public float wanderRadius;
    private float timer;
    private float movementDuration = 5.0f;
    private float waitBeforeMoving = 0.05f;
    private bool hasArrived = false;
 
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
 
        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            //float eased = Easing.Cubic.InOut(t);
            float eased = Easing.Quadratic.InOut(t);
            transform.position = Vector3.Lerp(startPos, targetPos, eased);
 
            yield return null;
        }
 
        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }
        return Vector3.zero;
    }
}
