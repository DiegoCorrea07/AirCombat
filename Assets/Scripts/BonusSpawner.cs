using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject energyDotPrefab; // Prefab de EnergyDot
    public Transform[] spawnPoints; // Puntos de aparici�n
    public float spawnInterval = 5f; // Intervalo entre apariciones

    private void Start()
    {
        InvokeRepeating("SpawnBonus", 1f, spawnInterval);
    }

    void SpawnBonus()
    {
        if (spawnPoints.Length > 0 && energyDotPrefab != null)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(energyDotPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
    }
}
