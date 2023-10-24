using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monster;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10,11),5,Random.Range(-10,11));
            Instantiate(monster,randomSpawnPosition, Quaternion.identity);
        }
    }
}
