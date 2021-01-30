using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour {
    float portalProgress;
    public bool doIncrease = false;
    public PortalEntity portalEntityPrefab;
    public PortalEntity currentPortal;

    public List<PortalEntity> oldPortals = new List<PortalEntity>();

    public float portalFillSpeed = 0.1f;
    public float portalActivationRange = 1.1f;
    public bool trigger;
    public Transform goblin;
    void Start() {
        goblin = FindObjectOfType<PlayerMovementHandler>().transform;
        EventCoordinator.StartListening(EventName.Input.Player.StartChannelingPortal(), OnStartPortal);
        EventCoordinator.StartListening(EventName.Input.Player.StopChannelingPortal(), OnStopPortal);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Player.StartChannelingPortal(), OnStartPortal);
        EventCoordinator.StopListening(EventName.Input.Player.StopChannelingPortal(), OnStopPortal);
    }
    void OnStartPortal(GameMessage msg) {
        trigger = true;
        CheckPortalReset(msg.coordinates);
    }
    void OnStopPortal(GameMessage msg) {
        trigger = false;
        if (currentPortal == null)
            return;
        if ((currentPortal.transform.position - msg.coordinates).magnitude > portalActivationRange) {
            portalProgress = 0f;
            DestroyCurrentPortal();
        }

    }
    void DestroyCurrentPortal() {
        oldPortals.Add(currentPortal);
        foreach (PortalEntity portal in oldPortals) {
            DestroyPortal(portal);
        }
        oldPortals.Clear();
    }
    void Update() {
        if (trigger) {
            portalProgress += Time.deltaTime * portalFillSpeed;
            SetProgress(currentPortal, portalProgress);
            if ((goblin.position - currentPortal.transform.position).magnitude > portalActivationRange) {
                trigger = false;
                DestroyCurrentPortal();
            }
        }
    }

    void CheckPortalReset(Vector3 pos) {
        if (currentPortal == null) {
            CreatePortal(pos);
        }

        if ((currentPortal.transform.position - pos).magnitude > portalActivationRange) {
            portalProgress = 0f;
            CreatePortal(pos);
        }
        foreach (PortalEntity portal in oldPortals) {
            DestroyPortal(portal);
        }
        oldPortals.Clear();
    }

    void CreatePortal(Vector3 playerPosition) {
        if (currentPortal != null)
            oldPortals.Add(currentPortal);
        currentPortal = Instantiate(portalEntityPrefab, PortalPosition(playerPosition), Quaternion.identity, transform);
        currentPortal.transform.position = playerPosition;
    }

    Vector3 PortalPosition(Vector3 playerPosition) {
        return playerPosition; // + Vector3.forward * 0.2f;
    }
    void SetProgress(PortalEntity portal, float progress) {
        portal.SetPortalProgress(progress);
    }
    void DestroyPortal(PortalEntity portal) {
        portal.DestroyPortal();
        Debug.Log("destroy portal");
    }
}