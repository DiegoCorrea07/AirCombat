using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScena : MonoBehaviour
{
    public Button startButton;  // Referencia al bot�n de iniciar
    public Button exitButton;   // Referencia al bot�n de salir
    public Button restartButton; // Referencia al bot�n de reiniciar

    void Start()
    {
        // Aseg�rate de que los botones est�n asociados
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame); // Asocia la funci�n StartGame al bot�n
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame); // Asocia la funci�n ExitGame al bot�n
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame); // Asocia la funci�n RestartGame al bot�n
        }
    }

    // Funci�n para iniciar el juego (ir a la escena "SampleScene")
    void StartGame()
    {
        // Aseg�rate de usar el nombre correcto de tu escena del juego
        SceneManager.LoadScene("SampleScene");
    }

    // Funci�n para salir del juego
    void ExitGame()
    {
        // Si estamos en una construcci�n de juego, cierra la aplicaci�n
        Application.Quit();

        // En el editor de Unity, podemos simular el cierre con esta l�nea (para pruebas):
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Esta funci�n reinicia la escena del juego
    void RestartGame()
    {
        // Aseg�rate de usar el nombre correcto de tu escena del juego
        SceneManager.LoadScene("SampleScene");
    }
}
