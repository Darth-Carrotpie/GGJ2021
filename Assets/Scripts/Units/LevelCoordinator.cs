using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCoordinator : MonoBehaviour, HeroVision
{
    public Transform  playerTransform;
    public Transform  heroTransform;
    List<Transform> lootTransform = new List<Transform>();
    List<Transform> mobTransform = new List<Transform>();
    
    public Transform GetPlayer()
    {
        return playerTransform;
    }
    public Transform GetClosestLoot()
    {
        if (lootTransform.Count == 0)
        {
            return null;
        }
        return lootTransform[0];
    }
    public Transform GetClosestMob()
    {
        if (mobTransform.Count == 0)
        {
            return null;
        }
        return mobTransform[0];
    }

    void Awake()
    {
        heroTransform.GetComponent<HeroBehaviour>().vision = this;
    }
}
