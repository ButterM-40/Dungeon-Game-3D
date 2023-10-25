using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnRange = 1f;
    private float destroyRange = 40f;
    public GameObject enemyPrefab;
 private Player_Spawner_Range player_spawner_range;
     Transform target1;
    float xPos;
    float zPos;
    float randomx;
    float randomz;

    private void OnEnable()
    {
    target1 = Player_Manager.instance.player.transform;
    xPos = transform.position.x;
    zPos = transform.position.z;
         for (int i = 0; i < 5; i++) // Spawn 5 enemies as an example
        {
            // Vector3 randomPosition = Random.insideUnitSphere * spawnRange;
            // randomPosition.y = -5; // Ensure enemies are spawned at the same height
            randomx = Random.Range(xPos, xPos+5);
            randomz = Random.Range(zPos, zPos+5);
            Instantiate(enemyPrefab, new Vector3(randomx,-4,randomz),Quaternion.identity);
        }
    Debug.Log("ITS ON AGAIN BABY");
    }
    private void OnDisable()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy"); // Assuming you've tagged your enemies as "Enemy"

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, target1.position);
            Debug.Log("DISTANCE: " + distance);
            if (distance > destroyRange)
            {
                Destroy(enemy);
            }
        }
    }
    // void Start()
    // {
    // target1 = Player_Manager.instance.player.transform;
    // xPos = transform.position.x;
    // zPos = transform.position.z;
    // SpawnEnemies();
    // }
    void Update()
    {
       // Debug.Log("TESTTT");
        //DestroyEnemiesOutOfRange();
    }
//   void SpawnEnemies()
//     {

//     }
//   void DestroyEnemiesOutOfRange()
//     {
//         GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy"); // Assuming you've tagged your enemies as "Enemy"

//         foreach (var enemy in enemies)
//         {
//             float distance = Vector3.Distance(enemy.transform.position, target1.position);
//             Debug.Log("DISTANCE: " + distance);
//             if (distance > destroyRange)
//             {
//                 Destroy(enemy);
//             }
//         }
//     }
    // Update is called once per frame

}
