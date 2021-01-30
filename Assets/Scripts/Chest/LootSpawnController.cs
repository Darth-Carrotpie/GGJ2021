using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawnController : MonoBehaviour {
    public GameObject lootSpawnPointPrefab;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Economy.ChestWasOpened(), OnChestOpwned);

        EventCoordinator.StartListening(EventName.System.Environment.MobKilled(), OnMobKilled);
    }

    void OnChestOpwned(GameMessage msg) {
        GameObject newSpawnPoint = Instantiate(lootSpawnPointPrefab);
        newSpawnPoint.transform.parent = transform.parent;
        newSpawnPoint.transform.position = msg.targetChest.transform.position;
    }

    void OnMobKilled(GameMessage msg) {
        GameObject newSpawnPoint = Instantiate(lootSpawnPointPrefab);
        newSpawnPoint.GetComponent<FountainSpawn>().numberOfItems = 3;
        newSpawnPoint.transform.parent = transform.parent;
        newSpawnPoint.transform.position = msg.targetTransform.position;
    }
}