using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHealth : MonoBehaviour
{
    private int health = 1;

    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.Damage(), OnGotDamage);
    }

    void OnGotDamage(GameMessage msg) {
        
        if (transform == msg.targetTransform) {
            health -= 1;

            EventCoordinator.TriggerEvent(EventName.System.Environment.GoblinGotDamage(), GameMessage.Write());

            if (health == 0) {
                EventCoordinator.TriggerEvent(EventName.System.Environment.GoblinDied(), GameMessage.Write().WithTargetTransform(gameObject.transform));
            } 
        }
    }
}
