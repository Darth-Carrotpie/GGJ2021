using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleListenerSpawner : MonoBehaviour {
    public GameObject sphere;

    void Start() {
        EventCoordinator.StartListening(EventName.UI.ExampleEvent(), OnEv);
    }

    void OnEv(GameMessage msg) {
        for (int i = 0; i < msg.intMessage; i++) {
            GameObject newObj = Instantiate(sphere);
            newObj.transform.position = Vector3.up * Random.Range(0, 1.5f) + transform.position;
            Destroy(newObj, 2f);
        }
    }
}