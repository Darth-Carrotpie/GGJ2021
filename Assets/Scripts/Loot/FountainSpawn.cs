using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainSpawn : MonoBehaviour {

    public int numberOfItems;
    public float spawnRate = 10f;
    public GameObject itemPrefab;
    public List<Sprite> itemSprites;

    float timer = 0f;
    int count = 0;
    private void Update() {
        timer += Time.deltaTime * spawnRate;
        if (timer > 1f) {
            timer = 0;
            count++;
            SpawnItem();
        }
        if (count >= numberOfItems)
            Destroy(gameObject);
    }

    void SpawnItem() {
        GameObject newItem = Instantiate(itemPrefab);
        itemPrefab.transform.position = transform.position;
        newItem.GetComponentInChildren<SpriteRenderer>().sprite = itemSprites[Random.Range(0, itemSprites.Count)];
        newItem.transform.parent = transform.parent;
        newItem.GetComponent<LootFly>().ThrowAt(Vector3.zero);
    }
}