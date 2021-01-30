using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int health = 100;
    // Start is called before the first frame update
    void Start()
    {
         EventCoordinator.StartListening(EventName.System.Environment.Damage(), GetDamage);
    }

    // Update is called once per frame
    void Update()
    {       
    }

    void GetDamage(GameMessage msg)
    {
        print("Got damage");
        health -= 50;

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
