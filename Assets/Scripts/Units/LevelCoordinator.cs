using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCoordinator : Singleton<LevelCoordinator>
{
    public Transform playerTransform;
    public Transform heroTransform;
    public List<Transform> lootTransforms = new List<Transform>();
    public List<Transform> mobTransforms = new List<Transform>();

    public static Transform GetPlayer()
    {
        return Instance.playerTransform;
    }

    public static Transform GetClosestLoot(Transform from)
    {
        return Instance.lootTransforms.FindNearest(from);
    }

    public static Transform GetClosestMob(Transform from)
    {
        return Instance.mobTransforms.FindNearest(from);
    }
}
