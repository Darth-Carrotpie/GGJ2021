using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomChildFX : MonoBehaviour {
    List<Transform> children;
    void Start() {
        children = new List<Transform>(GetComponentsInChildren<Transform>(true));
        int i = Random.Range(0, children.Count);
        children[i].gameObject.SetActive(true);
        Destroy(gameObject, 2f);
    }
}