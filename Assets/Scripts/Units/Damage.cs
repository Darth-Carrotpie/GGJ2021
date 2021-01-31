using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("d"))
        {
            this.TestTriggerEventDamage();
        } */
    }

    public void TestTriggerEventDamage() 
    {
        EventCoordinator.TriggerEvent(EventName.System.Environment.Damage(), GameMessage.Write().WithStringMessage("D key pressed"));
    }
}
