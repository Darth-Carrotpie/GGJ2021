using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LootScript : MonoBehaviour
{
    void Start()
    {
        // Don't spawn items out of reach
        NavMeshHit navHit;
        if (!NavMesh.SamplePosition(transform.position, out navHit, 1.0f, NavMesh.AllAreas))
        {
            Destroy(gameObject);
        }

        EventCoordinator.StartListening(EventName.System.Environment.PickUpLoot(), OnLootPickUp);
    }

    void OnLootPickUp(GameMessage msg)
    {
        if (msg.targetTransform == transform)
        {
            Destroy(gameObject);
        }
    }
}
