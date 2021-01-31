using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGameButtonClick : MonoBehaviour {
    public void OnButtonClickLoadScene() {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}