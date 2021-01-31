using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour {
    Image image;
    private int health = 10;
    
    void Start() {
        image = GetComponent<Image>();
        image.fillAmount = 10;
        EventCoordinator.StartListening(EventName.System.Environment.Damage(), OnGoblinGotDamage);
    }

    void OnGoblinGotDamage(GameMessage msg) {
        if (transform == msg.targetTransform) {
            image.fillAmount -= 1;
            health -= 1;

            if (health == 0) {
                EventCoordinator.TriggerEvent(EventName.System.Environment.GoblinDied(), GameMessage.Write().WithTargetTransform(gameObject.transform));
            }   
        }
    }
}
