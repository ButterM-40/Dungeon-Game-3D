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
    public GameObject Enemy_Skeleton;
    public GameObject Enemy_Goblin;
 private Player_Spawner_Range player_spawner_range;
     Transform target1;
    float xPos;
    float zPos;
    float ypos;
    float randomx;
    float randomz;

    private void OnEnable()
    {
    target1 = Player_Manager.instance.player.transform;
    xPos = transform.position.x;
    zPos = transform.position.z;
    ypos = transform.position.y;
            // Vector3 randomPosition = Random.insideUnitSphere * spawnRange;
            // randomPosition.y = -5; // Ensure enemies are spawned at the same height
            randomx = Random.Range(xPos, xPos+5);
            randomz = Random.Range(zPos, zPos+5);
            int enemy1Count = Random.Range(0, 5 + 1); // Random count of enemy1
            int enemy2Count = 5 - enemy1Count; // Remaining count for enemy2
            Spawner_Enemy(enemy1Count, Enemy_Skeleton);
            Spawner_Enemy(enemy2Count, Enemy_Goblin);
          //  Instantiate(enemyPrefab, new Vector3(randomx,ypos,randomz),Quaternion.identity);
          // Debug.Log("ITS ON AGAIN BABY");
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
    void Spawner_Enemy(int count, GameObject enemyType)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRange;
            spawnPosition.y = 0; // Ensure the enemies spawn on the same plane

            Instantiate(enemyType, spawnPosition, Quaternion.identity);
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
