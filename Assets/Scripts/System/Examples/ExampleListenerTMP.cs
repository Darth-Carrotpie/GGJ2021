using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ExampleListenerTMP : MonoBehaviour {
    TextMeshProUGUI tmpObject;
    void Start() {
        tmpObject = GetComponent<TextMeshProUGUI>();
        EventCoordinator.StartListening(EventName.UI.ExampleEvent(), OnButtonClickEventWhateverNameExampleHere);
    }

    void OnButtonClickEventWhateverNameExampleHere(GameMessage msg) //always has to receive a message
    {
        tmpObject.text = msg.intMessage.ToString();
    }
}