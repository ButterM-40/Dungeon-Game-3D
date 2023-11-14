
using UnityEngine;

public class CrystalThrow : MonoBehaviour
{
    public Transform player;
   public GameObject[] crystalPrefabs; // Array of crystal prefabs to choose from.
   public GameObject crystalsParent;
    public int numberOfCrystals = 10; // The number of crystals in the ring.
    public float ringRadius = 5f; // The radius of the ring.
    public float ringHeight = 2f; // The height at which the ring should appear.

     private void Start()
    {
        Vector3 initialPosition = transform.position;
        initialPosition.y = 3f; // Set the y-axis position to 3.
        crystalsParent.transform.position = initialPosition;
        CreateCrystalRing();
        //crystalsParent.transform.rotation = Quaternion.Euler(-90f, 90f, -90f);
    }

    private void CreateCrystalRing()
    {
        float heightOffset = 1.0f; // Adjust the height offset as needed.

        for (int i = 0; i < numberOfCrystals; i++)
        {
            int crystalIndex = Random.Range(0, crystalPrefabs.Length); // Choose a random crystal prefab from the array.

            float angle = i * (360f / numberOfCrystals); // Calculate the angle for even distribution.

            // Calculate the position in the ring around the y-axis with a height offset.
            float x = ringRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = ringRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
            float y = heightOffset;

            Vector3 spawnPosition = crystalsParent.transform.position + new Vector3(x, y, z);

            Quaternion spawnRotation = Quaternion.Euler(0f, angle, 0f);

            GameObject crystal = Instantiate(crystalPrefabs[crystalIndex], spawnPosition, spawnRotation);
            //crystal.AddComponent<CrystalHoming>();
            CrystalHoming cardScript = crystal.AddComponent<CrystalHoming>();
            cardScript.SetParameters(player);
            // Set the crystalsParent GameObject as the parent of the crystal.
            //crystal.transform.SetParent(crystalsParent.transform);
            crystal.transform.SetParent(null);
        }
    }
    public void FireCrystal(){
        // foreach(Transform child in transform)
        // {
        //     Something(child.gameObject);
        // }
        Debug.Log("Throw");
    }
    // EInumerator(GameObject cyst){

    // }
}