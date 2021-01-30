using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    void Start()
    {
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
