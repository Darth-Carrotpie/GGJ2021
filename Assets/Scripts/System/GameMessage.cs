using System.Collections.Generic;
using UnityEngine;
public class GameMessage {
    public static GameMessage Write() {
        return new GameMessage();
    }
    public string strMessage;
    public GameMessage WithStringMessage(string value) {
        strMessage = value;
        return this;
    }

    public Vector3 coordinates;
    public GameMessage WithCoordinates(Vector3 value) {
        coordinates = value;
        return this;
    }
    public Vector2 direction;
    public GameMessage WithDirection(Vector2 value) {
        direction = value;
        return this;
    }

    public int intMessage;
    public GameMessage WithIntMessage(int value) {
        intMessage = value;
        return this;
    }
    public bool state;
    public GameMessage WithState(bool value) {
        state = value;
        return this;
    }

    public float floatMessage;
    public GameMessage WithFloatMessage(float value) {
        floatMessage = value;
        return this;
    }
    public float targetFloatMessage;
    public GameMessage WithTargetFloat(float value) {
        targetFloatMessage = value;
        return this;
    }
    public float deltaFloat;
    public GameMessage WithDeltaFloat(float value) {
        deltaFloat = value;
        return this;
    }

    public Transform targetTransform;
    public GameMessage WithTargetTransform(Transform value) {
        targetTransform = value;
        return this;
    }
    public ChestEntity targetChest;
    public GameMessage WithChest(ChestEntity value) {
        targetChest = value;
        return this;
    }
    public PortalEntity targetPortal;
    public GameMessage WithPortal(PortalEntity value) {
        targetPortal = value;
        return this;
    }
    public Vector3 targetCoordinates;
    public GameMessage WithTargetCoordinates(Vector3 value) {
        targetCoordinates = value;
        return this;
    }
}