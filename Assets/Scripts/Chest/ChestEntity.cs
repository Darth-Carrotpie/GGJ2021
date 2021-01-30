using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEntity : MonoBehaviour {
    SpriteRenderer renderer;
    bool isOpen = false;
    public Sprite open;
    void Start() {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Open() {
        renderer.sprite = open;
        if (!isOpen)
            EventCoordinator.TriggerEvent(EventName.System.Economy.ChestWasOpened(), GameMessage.Write().WithChest(this));
        isOpen = true;
    }
}