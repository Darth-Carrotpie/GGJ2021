using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject blood;
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.Damage(), GetDamage);
    }

    void Update()//For testing
    {
        if (Input.GetKeyDown("p"))
        {
            EventCoordinator.TriggerEvent(EventName.System.Environment.MobKilled(), GameMessage.Write().WithTargetTransform(gameObject.transform));

            Destroy(gameObject);
        }
    }
    void GetDamage(GameMessage msg)
    {
        if (gameObject.transform == msg.targetTransform)
        {
            EventCoordinator.TriggerEvent(EventName.System.Environment.MobKilled(), GameMessage.Write().WithTargetTransform(gameObject.transform));

            Destroy(gameObject);
        }
    }
}
