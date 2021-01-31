using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGameButtonClick : MonoBehaviour {
    public string sceneToLoad = "";
    public void OnButtonClickLoadScene() {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}