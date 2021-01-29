using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEventTrigger : MonoBehaviour {

    int size = 0;
    //Sets a local variable to send to an event
    public void ExampleSetSize(string amount) {
        size = int.Parse(amount);
    }

    //There has to be an EventCoordinator in the scene to call triggers!
    public void ExampleTriggerEvent() {
        EventCoordinator.TriggerEvent(EventName.UI.ExampleEvent(), GameMessage.Write().WithStringMessage("Example Button Clicked").WithIntMessage(size));
    }
}