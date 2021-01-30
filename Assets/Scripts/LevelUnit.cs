using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnit : MonoBehaviour
{
    public enum Type
    {
        Player,
        Loot,
        Hero,
        Mob
    }

    public Type type;

    void Awake()
    {
        switch (type)
        {
            case Type.Player:
                LevelCoordinator.Instance.playerTransform = transform;
                break;
            case Type.Loot:
                LevelCoordinator.Instance.lootTransforms.Add(transform);
                break;
            case Type.Hero:
                LevelCoordinator.Instance.heroTransform = transform;
                break;
            case Type.Mob:
                LevelCoordinator.Instance.mobTransforms.Add(transform);
                break;
        }
    }

    void OnDestroy()
    {
        if (LevelCoordinator.Instance == null)
        {
            return;
        }

        switch (type)
        {
            case Type.Player:
                LevelCoordinator.Instance.playerTransform = null;
                break;
            case Type.Loot:
                LevelCoordinator.Instance.lootTransforms.Remove(transform);
                break;
            case Type.Hero:
                LevelCoordinator.Instance.heroTransform = null;
                break;
            case Type.Mob:
                LevelCoordinator.Instance.mobTransforms.Remove(transform);
                break;
        }
    }

}
