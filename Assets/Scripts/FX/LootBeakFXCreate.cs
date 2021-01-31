using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBeakFXCreate : MonoBehaviour {
    public GameObject fxToCreate;
    void OnDestroy() {
        GameObject newFx = Instantiate(fxToCreate);
        newFx.transform.position = this.transform.position;
    }
}