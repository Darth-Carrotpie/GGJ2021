using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnUnits : MonoBehaviour
{
    private List<GameObject> mobList = new List<GameObject>();
    public GameObject Zombie;
    public GameObject Shaman;
    public GameObject Fallen;
    public GameObject Beast;
    public int count;
    private int mobIndex;

    void Start()
    {
        mobList.Add(Zombie);
        mobList.Add(Shaman);
        mobList.Add(Fallen);
        mobList.Add(Beast);

        GenerateObject();
    }

    void GenerateObject()
    {
        for (int i = 0; i < count; i++)
        {
            mobIndex = UnityEngine.Random.Range(0, 4);

            if (mobList[mobIndex] == null)
            {
                continue;
            }

            var pos = GetRandomPoint();
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(pos, out navHit, 1.0f, NavMesh.AllAreas))
            {
                GameObject tmp = Instantiate(mobList[mobIndex]);
                tmp.transform.SetParent(transform.parent);
                tmp.gameObject.transform.position = navHit.position;

                EventCoordinator.TriggerEvent(EventName.System.Environment.CreateMob(), GameMessage.Write().WithTargetTransform(tmp.transform));
            }
        }
    }

    Vector3 GetRandomPoint()
    {
        return RandomNavSphere(transform.position, 10);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randDirection = Random.insideUnitCircle * dist;

        return origin + new Vector3(randDirection.x, 0, randDirection.y);
    }
}
