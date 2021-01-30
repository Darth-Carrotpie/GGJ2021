using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    // Use this for initialization
    void Start() {
        PlayerMovementHandler player = FindObjectOfType<PlayerMovementHandler>();
        if (player != null)
            target = FindObjectOfType<PlayerMovementHandler>().transform;
        if (target == null)
            target = FindObjectOfType<HeroBehaviour>().transform;
        offset = transform.position - target.position;
        EventCoordinator.StartListening(EventName.System.Victory(), OnVictory);
    }

    // Update is called once per frame
    void LateUpdate() {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
    void OnVictory(GameMessage msg) {
        target = FindObjectOfType<HeroBehaviour>().transform;
    }
}