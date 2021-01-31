using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {

    public Vector2 currentDir;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.GoblinDied(), OnGoblinDeath);
    }
    void OnGoblinDeath(GameMessage msg) {
        Destroy(this);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            AddDir(Vector2.up);
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            RemoveDir(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            AddDir(Vector2.left);
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            RemoveDir(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            AddDir(Vector2.down);
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            RemoveDir(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            AddDir(Vector2.right);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            RemoveDir(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            //start channeling input
            EventCoordinator.TriggerEvent(EventName.Input.Player.StartChannelingPortal(), GameMessage.Write().WithCoordinates(transform.position));
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            //stop channeling input
            EventCoordinator.TriggerEvent(EventName.Input.Player.StopChannelingPortal(), GameMessage.Write().WithCoordinates(transform.position));
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            //open chest
            EventCoordinator.TriggerEvent(EventName.Input.Player.OpenChest(), GameMessage.Write().WithCoordinates(transform.position));
        }
        if (Input.GetMouseButtonDown(0)) {
            Vector3 coord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            EventCoordinator.TriggerEvent(EventName.Input.Player.ThrowLoot(), GameMessage.Write().WithCoordinates(transform.position).WithTargetCoordinates(coord));
        }
    }

    void AddDir(Vector2 dir) {
        currentDir += dir;
        EventCoordinator.TriggerEvent(EventName.Input.Player.Move(), GameMessage.Write().WithDirection(currentDir));
    }
    void RemoveDir(Vector2 dir) {
        currentDir -= dir;
        EventCoordinator.TriggerEvent(EventName.Input.Player.Move(), GameMessage.Write().WithDirection(currentDir));
    }
}