using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEntity : MonoBehaviour {
    public SpriteRenderer rend;
    bool destroyThisPortal;
    float destroyProgress = 1f;
    public float currentProgress;
    public float destructionSpeed = 2f;
    bool forward = false;
    void Start() {
        if (rend == null)
            rend = GetComponentInChildren<SpriteRenderer>();
        EventCoordinator.StartListening(EventName.System.Economy.PortalProgress(), OnProgress);
    }

    void Update() {
        if (destroyThisPortal) {

            if (destroyProgress <= 0) {
                forward = true;
            }
            if (forward) {
                destroyProgress += Time.deltaTime * destructionSpeed;
                rend.material.SetFloat("FillLevelB", destroyProgress);
            } else {
                destroyProgress -= Time.deltaTime * destructionSpeed;
                rend.material.SetFloat("FillLevelA", destroyProgress);
            }
            if (destroyProgress >= 1) {
                Destroy(this.gameObject);
            }
        }
    }
    void OnProgress(GameMessage msg) {
        SetPortalProgress(msg.floatMessage);
    }
    void SetPortalProgress(float progress) {
        currentProgress = 1 - progress;
        rend.material.SetFloat("FillLevelA", currentProgress);
        rend.material.SetFloat("FillLevelB", 0);
        if (currentProgress <= 0) {
            EventCoordinator.TriggerEvent(EventName.System.Victory(), GameMessage.Write().WithCoordinates(transform.position));
        }

    }
    public void DestroyPortal() {
        destroyThisPortal = true;
        destroyProgress = currentProgress;
    }
}