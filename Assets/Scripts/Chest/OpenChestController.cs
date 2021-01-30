using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class OpenChestController : MonoBehaviour {
    public float chestOpenRange = 2f;
    void Start() {
        EventCoordinator.StartListening(EventName.Input.Player.OpenChest(), OnOpenChestInput);
    }

    void OnOpenChestInput(GameMessage msg) {
        ChestEntity firstChestInRange = FindObjectsOfType<ChestEntity>().Where(x => (x.transform.position - transform.position).magnitude < chestOpenRange).FirstOrDefault();
        if (firstChestInRange != null)
            firstChestInRange.Open();
    }
}