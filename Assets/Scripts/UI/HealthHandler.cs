using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour {
    Image image;
    
    void Start() {
        image = GetComponent<Image>();
        image.fillAmount = 1;
        
        EventCoordinator.StartListening(EventName.System.Environment.GoblinGotDamage(), OnGoblinGotDamage);
    }

    void OnGoblinGotDamage(GameMessage msg) {
            image.fillAmount -= 0.1f;
    }
}
