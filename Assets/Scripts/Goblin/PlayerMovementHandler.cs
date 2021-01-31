using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementHandler : MonoBehaviour {
    Vector3 moveDirection;
    Vector3 previousMoveDirection;
    public float baseSpeed = 0.1f;
    public float collisionSize = 0.2f;
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
            Vector3 moveTo = previousMoveDirection;
            moveTo = CheckForObjects(moveTo);
            moveTo = CheckNavmesh(moveTo);
            moveTo = moveTo * actual * baseSpeed;
            transform.Translate(moveTo);
        }

        if (isMoving != stopTrigger) {
            stopTrigger = isMoving;
            if (!isMoving) {
                speed = 0;
                EventCoordinator.TriggerEvent(EventName.Input.Player.MovementStopped(), GameMessage.Write());
            }
        }
    }
    Vector3 CheckForObjects(Vector3 moveDir) {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, moveDir);
        Vector3 flatDir = moveDir;
        if (Physics.Raycast(ray, out hit)) {
            //if (hit.collider.isTrigger) {
            Vector3 incomingVec = hit.point - transform.position;
            if (incomingVec.magnitude < collisionSize) {
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
                Debug.DrawLine(transform.position, hit.point, Color.red, 3f);
                Debug.DrawRay(hit.point, reflectVec, Color.green, 3f);
                flatDir = (incomingVec + reflectVec).normalized;
                //Debug.DrawRay(transform.position, flatDir, Color.yellow);
            }
            //}
        }
        return flatDir;
    }
    Vector3 CheckNavmesh(Vector3 movedir) {
        NavMeshHit navHit;
        Vector3 flatDir = movedir;
        if (NavMesh.SamplePosition(movedir + transform.position, out navHit, 100, NavMesh.AllAreas)) {
            flatDir = navHit.position - transform.position;
            /*Vector3 incomingVec = navHit.position - transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, navHit.normal);
            flatDir = (incomingVec + reflectVec).normalized;*/
            Debug.DrawLine(transform.position, navHit.position, Color.blue, 3f);

        }
        return flatDir;
    }
}