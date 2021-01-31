using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Sprite[]   bloodSprite;
    public Transform  bloodPrefab;

    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.Damage(), GetDamage);
    }

    void Update()//For testing
    {
        if (Input.GetKeyDown("p"))
        {
            /*EventCoordinator.TriggerEvent(EventName.System.Environment.MobKilled(), GameMessage.Write().WithTargetTransform(gameObject.transform));

            Destroy(gameObject);*/
            GetDamage(GameMessage.Write().WithTargetTransform(transform));
        }
    }
    void GetDamage(GameMessage msg)
    {
        if (gameObject.transform == msg.targetTransform)
        {
            EventCoordinator.TriggerEvent(EventName.System.Environment.MobKilled(), GameMessage.Write().WithTargetTransform(gameObject.transform));

            Destroy(gameObject);

            Transform blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            blood.GetComponentInChildren<SpriteRenderer>().sprite = bloodSprite[Random.RandomRange(0, bloodSprite.Length-1)];
            blood.parent = transform.parent;
        }
    }
}
