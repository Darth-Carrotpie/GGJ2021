using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneShow : MonoBehaviour {
    bool trigger;
    float timer;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.GoblinDied(), OnGoblinDeath);
    }
    void OnGoblinDeath(GameMessage msg) {
        trigger = true;
    }
    // Update is called once per frame
    void Update() {
        if (trigger) {
            timer += Time.deltaTime;
            if (timer > 2f) {
                ShowEnd();
            }
        }
    }

    void ShowEnd() {
        VictoryCoordinator.SetDefeat();
        SceneManager.LoadScene("Post", LoadSceneMode.Single);
    }
}