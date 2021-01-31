using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public Transform bloodPrefab;
    public UnityEvent onDie;

    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.Damage(), GetDamage);
    }

    void Update()//For testing
    {
        /*if (Input.GetKeyDown("p"))
        {
            GetDamage(GameMessage.Write().WithTargetTransform(transform));
        }*/
    }
    void GetDamage(GameMessage msg)
    {
        if (gameObject.transform == msg.targetTransform)
        {
            EventCoordinator.TriggerEvent(EventName.System.Environment.MobKilled(), GameMessage.Write().WithTargetTransform(gameObject.transform));

            onDie.Invoke();
            Destroy(gameObject);

            Transform blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            blood.parent = transform.parent;
        }
    }
}
