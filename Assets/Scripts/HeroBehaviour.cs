using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    // Player/Loot/Mob, if null: immediate reevaluation
    private Transform target;

    [Tooltip("How long is one action done before reevaluating")]
    public float aggroDuration = 2;
    [Tooltip("The pause before reevaluating after completing a task")]
    public float thinkingDuration = 0.5f;
    [Tooltip("At what distance will the hero aggro on player or loot")]
    public float sightRadius = 5;
    [Tooltip("At what distance hero will start attacking")]
    public float attackRange = 2;
    [Tooltip("Interval between attacks")]
    public float attackTime = 1;
    [Tooltip("Distance at which item can be picked up")]
    public float pickUpRange = 2;
    [Tooltip("Time to pick up")]
    public float pickUpTime = 1;

    bool CanSee(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) < sightRadius;
    }

    bool CanAttack(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) < attackRange;
    }

    bool CanPickUp(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) < pickUpRange;
    }

    void ReevaluateTarget()
    {
        var player = LevelCoordinator.GetPlayer();
        var loot = LevelCoordinator.GetClosestLoot(transform);
        var mob = LevelCoordinator.GetClosestMob(transform);

        if (player == null)
        {
            Debug.LogWarning("No player found, player ded or scene was not initialised correctly");
            return;
        }

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
        if (CanSee(player) || mob == null)
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
        if (target)
        {
            StartCoroutine(Chase());
        }
        Debug.Log("reevaluated to " + state + target);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Wait());
    }

    IEnumerator Chase()
    {
        // Walk animation
        float startTime = Time.time;

        do
        {
            if (target == null)
            {
                StartCoroutine(Wait());
                yield break;
            }

            // Attack both player both loot
            if (state != State.LootAggro && CanAttack(target))
            {
                StartCoroutine(Attack());
                yield break;
            }

            // Attack both player both loot
            if (state == State.LootAggro && CanPickUp(target))
            {
                StartCoroutine(PickUpLoot());
                yield break;
            }

            agent.SetDestination(target.position);
            agent.isStopped = false;
            yield return null;
        }
        while (startTime + aggroDuration > Time.time);
        Reevaluate();
    }

    IEnumerator Wait()
    {
        // Wait animation
        float startTime = Time.time;
        agent.isStopped = true;

        do
        {
            yield return null;
        }
        while (startTime + aggroDuration > Time.time);
        Reevaluate();
    }

    IEnumerator Attack()
    {
        // Attack animation
        float startTime = Time.time;

        do
        {
            if (target == null)
            {
                StartCoroutine(Wait());
                yield break;
            }
            // TODO: stop while moving?
            agent.SetDestination(target.position);
            agent.isStopped = false;

            yield return null;
        }
        while (startTime + attackTime > Time.time);

        Debug.Log("hit");
        EventCoordinator.TriggerEvent(EventName.System.Environment.Damage(), GameMessage.Write().WithTargetTransform(target));
        Reevaluate();
    }

    IEnumerator PickUpLoot()
    {
        // PickUp animation
        float startTime = Time.time;

        do
        {
            if (target == null)
            {
                StartCoroutine(Wait());
                yield break;
            }

            yield return null;
        }
        while (startTime + pickUpTime > Time.time);

        Debug.Log("pick up");
        EventCoordinator.TriggerEvent(EventName.System.Environment.PickUpLoot(), GameMessage.Write().WithTargetTransform(target));
        Reevaluate();
    }
}
