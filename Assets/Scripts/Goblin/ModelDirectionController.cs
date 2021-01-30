using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDirectionController : MonoBehaviour {
    bool dir;
    bool isLeft;
    void Start() {
        EventCoordinator.StartListening(EventName.Input.Player.Move(), OnMoveEvent);
    }

    void OnMoveEvent(GameMessage msg) {
        if (msg.direction.x > 0)
            isLeft = false;
        if (msg.direction.x < 0)
            isLeft = true;
        float x = -1f;
        if (dir)
            x = 1f;
        if (isLeft != dir) {
            dir = isLeft;
            transform.localScale = new Vector3(x, 1, 1);
        }
    }
}