using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         EventCoordinator.StartListening(EventName.System.Environment.Damage(), GetDamage);
    }

    void GetDamage(GameMessage msg)
    {
        if (gameObject.transform == msg.targetTransform)
        {
            Destroy(gameObject);
        }
    }
}
