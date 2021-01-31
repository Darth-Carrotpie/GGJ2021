using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnUnits : MonoBehaviour
{
    private List<GameObject> mobList = new List<GameObject>();
    public  GameObject Zombie;
    public  GameObject Shaman;
    public  GameObject Fallen;
    public  GameObject Beast;
    public  int count;
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
            mobIndex = UnityEngine.Random.Range(0,4);

            if (mobList[mobIndex] == null) 
            {
                continue;
            }

            GameObject tmp = Instantiate(mobList[mobIndex]);
            
            tmp.transform.SetParent(transform.parent);

            Vector3 randomPoint = GetRandomPoint();
            tmp.gameObject.transform.position = new Vector3(randomPoint.x, tmp.transform.position.y, randomPoint.z);

            EventCoordinator.TriggerEvent(EventName.System.Environment.CreateMob(), GameMessage.Write().WithTargetTransform(tmp.transform));
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 newPos = RandomNavSphere(transform.position, 10, -1);

        return newPos + transform.position;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
    {
        NavMeshHit  navHit;
        bool        canMove         = false;
        Vector3     randDirection   = Random.insideUnitCircle * dist;

        randDirection += origin;

        if (!NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            while (!canMove)
            {
                randDirection = Random.insideUnitCircle * dist;

                randDirection += origin;

                canMove = NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
            }
        }

        return navHit.position;
    }
}
