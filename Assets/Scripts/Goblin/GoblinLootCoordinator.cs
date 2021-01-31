using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinLootCoordinator : Singleton<GoblinLootCoordinator> {
    int currentLootAmount = 1;

    public static int GetLootAmount() {
        return Instance.currentLootAmount;
    }
    public static bool HasLoot() {
        return Instance.currentLootAmount > 0;
    }
    public static void IncreaseLoot(int amount) {
        Instance.currentLootAmount += amount;
    }

    public static void DecreaseLoot(int amount) {
        Instance.currentLootAmount -= amount;
    }
}