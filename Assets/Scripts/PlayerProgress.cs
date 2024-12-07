using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    public Slider energyBar;          // La barra de progreso
    public int maxEnergy = 10;       // Energ�a m�xima
    private int currentEnergy = 0;    // Energ�a actual
    public int levelNumber = 1;

    void Start()
    {
        // Configurar el Slider
        energyBar.maxValue = maxEnergy;
        energyBar.value = 0; // La barra comienza vac�a
    }

    public void AddEnergy()
    {
        currentEnergy += 1; // Incrementa la energ�a en 1
        energyBar.value = currentEnergy; // Actualiza la barra

        if (IsLevelCompleted())
        {
            Debug.Log("�Nivel completado!");
        }
    }

    public bool IsLevelCompleted()
    {
        return currentEnergy >= maxEnergy; // Devuelve true si se completa el nivel
    }


    public void ResetProgress()
    {
        currentEnergy = 0; // Reinicia la energ�a actual
        energyBar.value = currentEnergy; // Reinicia la barra
    }
}
