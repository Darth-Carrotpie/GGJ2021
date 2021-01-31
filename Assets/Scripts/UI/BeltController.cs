using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltController : MonoBehaviour {
    public List<Image> slots;
    public List<Sprite> itemSprites;

    void Start() {
        EventCoordinator.StartListening(EventName.System.Economy.AmountLootedChanged(), AmountLootedChanged);
    }

    void AmountLootedChanged(GameMessage msg) {
        int filledSlots = msg.intMessage;
        for (int i = 0; i < slots.Count; i++) {
            if (i < Mathf.RoundToInt(filledSlots / 2f)) {
                if (!slots[i].gameObject.active) {
                    slots[i].gameObject.SetActive(true);
                    slots[i].sprite = itemSprites[Random.Range(0, itemSprites.Count)];
                }
            } else {
                if (slots[i].gameObject.active) {
                    slots[i].gameObject.SetActive(false);
                }
            }
        }
    }
}