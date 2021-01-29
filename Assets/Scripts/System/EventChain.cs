using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventChain : MonoBehaviour {
    //This script 
    void Start() {
        EventCoordinator.Attach(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.Attach(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    void OnDestroy() {
        EventCoordinator.Detach(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.Detach(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    void OnStartGame(GameMessage msg) {
        //EventCoordinator.TriggerEvent(EventName.System.Environment.Initialized(), msg);
    }
    void OnEndMatch(GameMessage msg) {
        //EventCoordinator.TriggerEvent(EventName.UI.ShowScoreScreen(), msg);
    }
}