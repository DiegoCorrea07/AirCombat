using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScena : MonoBehaviour
{
    public Button startButton;  // Referencia al botón de iniciar
    public Button exitButton;   // Referencia al botón de salir
    public Button restartButton; // Referencia al botón de reiniciar

    void Start()
    {
        // Asegúrate de que los botones estén asociados
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame); // Asocia la función StartGame al botón
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame); // Asocia la función ExitGame al botón
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame); // Asocia la función RestartGame al botón
        }
    }

    // Función para iniciar el juego (ir a la escena "SampleScene")
    void StartGame()
    {
        // Asegúrate de usar el nombre correcto de tu escena del juego
        SceneManager.LoadScene("SampleScene");
    }

    // Función para salir del juego
    void ExitGame()
    {
        // Si estamos en una construcción de juego, cierra la aplicación
        Application.Quit();

        // En el editor de Unity, podemos simular el cierre con esta línea (para pruebas):
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Esta función reinicia la escena del juego
    void RestartGame()
    {
        // Asegúrate de usar el nombre correcto de tu escena del juego
        SceneManager.LoadScene("SampleScene");
    }
}
