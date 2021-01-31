using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PouchHandler : MonoBehaviour {
    TextMeshProUGUI text;
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        EventCoordinator.StartListening(EventName.System.Economy.AmountLootedChanged(), AmountLootedChanged);
    }

    void AmountLootedChanged(GameMessage msg) {
        text.text = "X" + GoblinLootCoordinator.GetLootAmount();
    }
}