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

    public Vector2 coordinates;
    public GameMessage WithCoordinates(Vector2 value) {
        coordinates = value;
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

}