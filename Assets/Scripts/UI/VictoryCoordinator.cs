using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCoordinator : Singleton<VictoryCoordinator> {
    bool isVictorious;
    public static void SetVictory() {
        Instance.isVictorious = true;
    }
    public static bool IsVictory() {
        return Instance.isVictorious;
    }
    public static void SetDefeat() {
        Instance.isVictorious = false;
    }
}