using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaHandler : MonoBehaviour {
    Image image;
    void Start() {
        image = GetComponent<Image>();
        EventCoordinator.StartListening(EventName.System.Economy.PortalProgress(), OnPortalProgress);
    }

    void OnPortalProgress(GameMessage msg) {
        image.fillAmount = msg.floatMessage;
    }
}