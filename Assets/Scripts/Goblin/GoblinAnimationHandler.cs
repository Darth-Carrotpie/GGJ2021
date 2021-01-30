using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimationHandler : MonoBehaviour {
    Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
        EventCoordinator.StartListening(EventName.Input.Player.Move(), OnMoveEvent);
        EventCoordinator.StartListening(EventName.Input.Player.OpenChest(), OnOpenChest);
        EventCoordinator.StartListening(EventName.Input.Player.StartChannelingPortal(), OnPortalStart);
        EventCoordinator.StartListening(EventName.Input.Player.StopChannelingPortal(), OnPortalStop);
    }

    void OnMoveEvent(GameMessage msg) {
        if (msg.direction.magnitude > 0)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);
    }
    void OnOpenChest(GameMessage msg) {
        animator.SetTrigger("loot");
    }
    void OnPortalStart(GameMessage msg) {
        animator.SetBool("portal", true);
    }
    void OnPortalStop(GameMessage msg) {
        animator.SetBool("portal", false);
    }
}