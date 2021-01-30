using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawnController : MonoBehaviour {
    public GameObject lootSpawnPointPrefab;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Economy.ChestWasOpened(), OnChestOpwned);
        //event when zombie killed?
        //here
    }

    void OnChestOpwned(GameMessage msg) {
        GameObject newSpawnPoint = Instantiate(lootSpawnPointPrefab);
        newSpawnPoint.transform.parent = transform.parent;
        newSpawnPoint.transform.position = msg.targetChest.transform.position;
    }
}