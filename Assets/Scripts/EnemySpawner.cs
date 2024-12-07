using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array para los diferentes tipos de enemigos
    public Transform[] spawnPoints; // Puntos de aparición para los enemigos
    public float spawnInterval = 2f; // Intervalo de generación de enemigos
    public int initialEnemyCount = 2; // Enemigos iniciales en el nivel 1
    private int currentEnemyCount; // Enemigos que deberían estar presentes en la escena
    public TextMeshProUGUI levelText; // Referencia al TextMeshProUGUI donde se muestra el nivel
    public TextMeshProUGUI scoreText; // Referencia para mostrar el puntaje
    private int levelNumber = 1; // Nivel actual
    private int score = 0; // Puntaje inicial

    public PlayerProgress playerProgress; // Referencia al script PlayerProgress

    private bool levelInProgress = false; // Bandera para evitar llamadas repetidas a LevelUp

    void Start()
    {
        currentEnemyCount = initialEnemyCount; // Comienza con el número inicial de enemigos
        SpawnInitialEnemies(); // Genera los enemigos iniciales

        // Comienza la generación periódica de enemigos
        InvokeRepeating("CheckAndMaintainEnemies", 1f, spawnInterval); // Revisa si hay suficientes enemigos y genera uno nuevo

        UpdateScoreText(); // Muestra el puntaje inicial
    }

    void Update()
    {
        // Verifica si el nivel se completa
        if (playerProgress != null && playerProgress.IsLevelCompleted() && !levelInProgress)
        {
            LevelUp(); // Sube de nivel si se completa
            UpdateLevelText(); // Actualiza el texto de nivel
        }
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = levelNumber.ToString(); // Muestra el nivel en el Text
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Muestra el puntaje actual
        }
    }

    void SpawnInitialEnemies()
    {
        // Genera enemigos según la cantidad actual de enemigos
        for (int i = 0; i < currentEnemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefabs.Length == 0) return; // Verificación básica

        // Selecciona un tipo de enemigo y un punto de aparición aleatorio
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length); // Selecciona un prefab de enemigo al azar
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length); // Selecciona un punto de aparición al azar

        GameObject newEnemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);

        // Suscribir al evento OnDestroyed del enemigo
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.OnDestroyed += HandleEnemyDestroyed; // Cuando el enemigo se destruye, genera uno nuevo
        }
    }

    void HandleEnemyDestroyed(GameObject enemy)
    {
        // Asegurarse de destruir el enemigo y luego generar uno nuevo
        Destroy(enemy); // Elimina el enemigo de la escena

        // Incrementar el puntaje
        AddScore(10);

        // Generar un nuevo enemigo para mantener la cantidad deseada en la escena
        SpawnEnemy();
    }

    void AddScore(int points)
    {
        score += points; // Suma los puntos al puntaje total
        UpdateScoreText(); // Actualiza el texto de puntaje
    }

    void CheckAndMaintainEnemies()
    {
        // Revisa si el número actual de enemigos es menor que el número deseado
        int activeEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        while (activeEnemies < currentEnemyCount)
        {
            SpawnEnemy(); // Genera un nuevo enemigo si es necesario
            activeEnemies++; // Incrementa el conteo manualmente para evitar bucles infinitos
        }
    }

    void LevelUp()
    {
        // Marcar que el nivel está en progreso para evitar repetición
        levelInProgress = true;

        levelNumber++; // Incrementa el nivel actual
        playerProgress.ResetProgress(); // Resetea la barra para el siguiente nivel

        // Incrementa el número de enemigos en función del nivel actual
        currentEnemyCount = initialEnemyCount + levelNumber - 1; // Aumenta un enemigo por cada nivel

        // Genera nuevos enemigos si el nivel lo requiere
        CheckAndMaintainEnemies();

        // Reestablecer la bandera al finalizar el proceso de nivel
        levelInProgress = false;
    }
}
