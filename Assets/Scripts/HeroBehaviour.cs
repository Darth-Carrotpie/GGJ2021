using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface HeroVision
{
    Transform GetPlayer();
    Transform GetClosestLoot();
    Transform GetClosestMob();
}

[RequireComponent(typeof(NavMeshAgent))]
public class HeroBehaviour : MonoBehaviour
{
    private enum State
    {
        PlayerAggro,
        LootAggro,
        MobAggro
    }

    NavMeshAgent agent;
    private State state = State.MobAggro;
    private float reevaluateAt;
    // Player/Loot/Mob, if null: immediate reevaluation
    private Transform target;

    [Tooltip("How long is one action done before reevaluating")]
    public float aggroDuration = 2;
    [Tooltip("The pause before reevaluating after completing a task")]
    public float thinkingDuration = 0.5f;
    [Tooltip("At what distance will the hero aggro on player or loot")]
    public float sightRadius = 5;
    [Tooltip("Gives targets to the hero")]
    public HeroVision vision;

    bool CanSee(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) < sightRadius;
    }

    void ReevaluateTarget()
    {
        var player = vision.GetPlayer();
        var loot = vision.GetClosestLoot();
        var mob = vision.GetClosestMob();
        Debug.Assert(player != null, "Player should always be defined when calling Reevaluate");

        if (state == State.PlayerAggro)
        {
            // If player is killed, this function should not be called
            Debug.Assert(target != null, "Target should always be defined when aggroed to a player");

            // Can only be compelled by loot in vision
            if (loot != null && CanSee(loot))
            {
                state = State.LootAggro;
                target = loot;
            }
            return;
        }

        // Once aggroed to loot or mob, it won't retarget until its picked up or killed
        if (target != null)
        {
            return;
        }

        if (loot != null && CanSee(loot))
        {
            state = State.LootAggro;
            target = loot;
            return;
        }

        // If there is no more mobs on the map, always targets player unless there is loot
        if (CanSee(player) || mob is null)
        {
            state = State.PlayerAggro;
            target = player;
            return;
        }

        if (mob != null && CanSee(mob))
        {
            state = State.MobAggro;
            target = mob;
            return;
        }
    }

    void Reevaluate()
    {
        ReevaluateTarget();
        reevaluateAt = Time.time + aggroDuration;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Reevaluate();
    }

    // Update is called once per frame
    void Update()
    {
        if (reevaluateAt < Time.time)
        {
            Reevaluate();
        }

        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    // Stop AI
    void OnPlayerKilled()
    {
        target = null;
        reevaluateAt = float.PositiveInfinity;
    }

    void OnLootPickedUp(Transform loot)
    {
        if (loot == target)
        {
            target = null;
            reevaluateAt = Time.time + thinkingDuration;
        }
    }

    void OnMobKilled(Transform mob)
    {
        if (mob == target)
        {
            target = null;
            reevaluateAt = Time.time + thinkingDuration;
        }
    }
}
