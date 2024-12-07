using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs; // Array de prefabs de nubes
    public float spawnInterval = 2f; // Intervalo entre apariciones
    public float minHeight = -2f; // Altura mínima para las nubes
    public float maxHeight = 2f; // Altura máxima para las nubes
    public float startX = 10f; // Posición X inicial para las nubes

    void Start()
    {
        // Iniciar el spawner en intervalos regulares
        InvokeRepeating("SpawnCloud", 0f, spawnInterval);
    }

    void SpawnCloud()
    {
        if (cloudPrefabs.Length == 0) return; // Evitar errores si no hay prefabs asignados

        // Seleccionar un prefab de nube aleatoriamente
        int randomIndex = Random.Range(0, cloudPrefabs.Length);
        GameObject selectedCloud = cloudPrefabs[randomIndex];

        // Generar una posición aleatoria en el eje Y
        float randomY = Random.Range(minHeight, maxHeight);

        // Instanciar la nube en la posición inicial
        Vector2 spawnPosition = new Vector2(startX, randomY);
        Instantiate(selectedCloud, spawnPosition, Quaternion.identity);
    }
}
