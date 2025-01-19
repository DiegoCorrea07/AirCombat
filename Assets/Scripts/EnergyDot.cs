using UnityEngine;

public class EnergyDot : MonoBehaviour
{
    private PlayerProgress playerProgress;

    void Start()
    {
        // Buscar automáticamente el script PlayerProgress en la escena
        playerProgress = FindObjectOfType<PlayerProgress>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")) // Verificar si colisiona con el jugador
    //    {
    //        if (playerProgress != null)
    //        {
    //            playerProgress.AddEnergy(); // Incrementa la barra de energía
    //        }

    //        Destroy(gameObject); // Elimina el EnergyDot
    //    }
    //}


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
}

