using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Vida máxima del jugador
    private float currentHealth; // Vida actual del jugador
    public Slider healthBar; // Referencia a la barra de vida
    //public Transform healthBarPosition; // Posición donde se mostrará la barra de vida (sobre el jugador)

    void Update()
    {
        // Actualiza la posición de la barra de vida sobre el jugador
        //if (healthBarPosition != null && healthBar != null)
        //{
        //    Vector3 screenPos = Camera.main.WorldToScreenPoint(healthBarPosition.position);
        //    healthBar.transform.position = screenPos; // Coloca la barra sobre el jugador
        //}
    }

    void Start()
    {
        currentHealth = maxHealth; // Inicia con la vida completa
        UpdateHealthBar(); // Actualiza la barra de vida al inicio
    }




    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce la vida
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que no baje de 0 ni pase del máximo
        UpdateHealthBar(); // Actualiza la barra de vida

        if (currentHealth <= 0)
        {
            Die(); // Llama al método de muerte si la vida llega a 0
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Ajusta el valor del Slider
        }
    }


    private void Die()
    {
        Debug.Log("¡El jugador ha muerto!");
        Destroy(gameObject); // Destruye el jugador
        SceneManager.LoadScene("GameOver"); // Carga la escena de Game Over
    }

}
