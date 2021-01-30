using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : MonoBehaviour
{
    public GameObject zombie;
    public int numberOfZombies;
    // Start is called before the first frame update
    void Start()
    {
        GenerateObject(zombie, numberOfZombies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateObject(GameObject go, int amount)
    {        
        if (go == null) return;

        for(int i = 0; i < amount; i++)
        {
            GameObject tmp = Instantiate(go);
            
            Vector3 randomPoint = GetRandomPoint();
            tmp.gameObject.transform.position = new Vector3(randomPoint.x, tmp.transform.position.y, randomPoint.z); 
        }
    }

    Vector3 GetRandomPoint()
    {
        int xRandom = 0;
        int zRandom = 0;

        xRandom = (int)Random.Range(-5, 5);
        zRandom = (int)Random.Range(-5, 5);

        return new Vector3(xRandom, 0.0f, zRandom);
    }
}
