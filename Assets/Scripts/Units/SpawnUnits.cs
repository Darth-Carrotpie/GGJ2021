using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnUnits : MonoBehaviour
{
    public enum SpawnType
    {
        Mob, Loot
    };
    public SpawnType spawnType;
    public GameObject spawnPrefab;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        GenerateObject(spawnPrefab, count);
    }

    void GenerateObject(GameObject go, int amount)
    {
        if (go == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject tmp = Instantiate(go);
            
            tmp.transform.SetParent(transform.parent);

            Vector3 randomPoint = GetRandomPoint();
            tmp.gameObject.transform.position = new Vector3(randomPoint.x, tmp.transform.position.y, randomPoint.z);

            if (spawnType == SpawnType.Mob)
            {
                EventCoordinator.TriggerEvent(EventName.System.Environment.CreateMob(), GameMessage.Write().WithTargetTransform(tmp.transform));
            }
            else
            {
                EventCoordinator.TriggerEvent(EventName.System.Environment.CreateLoot(), GameMessage.Write().WithTargetTransform(tmp.transform));
            }

        }
    }

    Vector3 GetRandomPoint()
    {
        /*int xRandom = 0;
        int zRandom = 0;

        xRandom = (int)Random.Range(-5, 5);
        zRandom = (int)Random.Range(-5, 5);
        
        return new Vector3(xRandom, 0.0f, zRandom) + transform.position;
        */
        Vector3 newPos = RandomNavSphere(transform.position, 10, -1);

        return newPos + transform.position;
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
    {
        Vector3 randDirection = Random.insideUnitCircle * dist;

        randDirection += origin;

        NavMeshHit navHit;
 
        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }
        return Vector3.zero;
        //NavMesh.FindClosestEdge(randDirection, out navHit, -1);  
    }
}
