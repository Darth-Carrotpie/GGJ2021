using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowController : MonoBehaviour {
    public GameObject lootPrefab;
    public List<Sprite> itemSprites;

    void Start() {
        EventCoordinator.StartListening(EventName.Input.Player.ThrowLoot(), OnLootThrow);
    }

    void OnLootThrow(GameMessage msg) {
        if (GoblinLootCoordinator.HasLoot()) {
            GameObject newThrown = Instantiate(lootPrefab);
            newThrown.transform.position = msg.coordinates;
            newThrown.GetComponent<LootFly>().ThrowAt(msg.targetCoordinates);
            GoblinLootCoordinator.DecreaseLoot(1);
            //Debug.Log("target coords: " + msg.targetCoordinates);
            newThrown.GetComponentInChildren<SpriteRenderer>().sprite = itemSprites[Random.Range(0, itemSprites.Count)];
        }
    }
}