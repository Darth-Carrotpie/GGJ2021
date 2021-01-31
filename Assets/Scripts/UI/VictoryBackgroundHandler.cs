using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VictoryBackgroundHandler : MonoBehaviour {
    public Sprite victory;
    public Sprite defeat;
    void Start() {
        Image myImg = GetComponent<Image>();
        if (VictoryCoordinator.IsVictory()) {
            myImg.sprite = victory;
        } else {

            myImg.sprite = defeat;
        }
    }
}