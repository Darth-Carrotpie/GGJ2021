using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour {
    Vector3 moveDirection;
    Vector3 previousMoveDirection;
    public float baseSpeed = 0.1f;
    float speed = 0f;
    bool stopTrigger = false;
    bool isMoving;
    void Start() {
        EventCoordinator.StartListening(EventName.Input.Player.Move(), OnMoveEvent);
    }
    void OnMoveEvent(GameMessage msg) {
        moveDirection = new Vector3(msg.direction.x, 0, msg.direction.y).normalized;
    }
    void Update() {
        if (moveDirection.magnitude > 0) {
            previousMoveDirection = moveDirection;
            isMoving = true;
            if (speed < 1f)
                speed += Time.deltaTime * 4f;
        } else {
            if (speed > 0)
                speed -= Time.deltaTime * 4f;

            if (speed <= 0) {
                isMoving = false;
            }
        }
        if (speed > 0) {
            float actual = Easing.Quadratic.Out(speed);
            transform.Translate(previousMoveDirection * actual * baseSpeed);
        }

        if (isMoving != stopTrigger) {
            stopTrigger = isMoving;
            if (!isMoving)
                EventCoordinator.TriggerEvent(EventName.Input.Player.MovementStopped(), GameMessage.Write());
        }
    }
}