using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KristupoTestas : MonoBehaviour, HeroVision
{
    public Transform player;
    public Transform loot;
    public Transform mob;
    public HeroBehaviour hero;

    public Transform GetPlayer()
    {
        return player;
    }

    public Transform GetClosestLoot()
    {
        return loot;
    }

    public Transform GetClosestMob()
    {
        return mob;
    }

    void Awake()
    {
        hero.vision = this;
    }
}
