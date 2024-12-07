using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 2f; // Velocidad de las nubes
    public float resetPositionX = -10f; // Posición X donde la nube se resetea
    public float startPositionX = 10f; // Posición X donde reaparece la nube

    void Update()
    {
        // Mover la nube hacia la izquierda
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Si la nube sale de la pantalla por la izquierda, reubícala
        if (transform.position.x <= resetPositionX)
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        // Reubicar la nube al lado derecho para que reaparezca
        transform.position = new Vector2(startPositionX, transform.position.y);
    }
}
