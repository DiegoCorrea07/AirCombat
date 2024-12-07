using UnityEngine;

public class BulletControllerEnemy : MonoBehaviour
{
    public float speed = 10f; // Velocidad constante de la bala
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    //public Animator bulletAnimator; // Referencia al Animator de la bala
    public AudioClip shootSound; // Sonido de disparo
    public AudioClip explosionSound; // Sonido de explosi�n
    private AudioSource audioSource; // Componente de AudioSource
    public float shootVolume = 1f; // Control del volumen para el disparo
    public float explosionVolume = 1f; // Control del volumen para la explosi�n
    public GameObject explosionPrefab; // Prefab de explosi�n

    private bool hasHit = false;

    void Start()
    {
        // Obtener el Rigidbody2D de la bala
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (rb == null)
        {
            Debug.LogError("El prefab de la bala necesita un Rigidbody2D.");
            return;
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.volume = shootVolume; // Establecer el volumen del disparo
            audioSource.PlayOneShot(shootSound); // Reproducir el sonido de disparo
        }

        // El movimiento de la bala ser� controlado por el Rigidbody2D
        // Este script no maneja la velocidad directamente, ya que lo puedes manejar desde otro lado (Enemy o Player)
    }

    void Update()
    {
        // Este m�todo ahora est� vac�o porque el movimiento de la bala ya depende del Rigidbody2D.
    }

    private void OnBecameInvisible()
    {
        // Destruir la bala cuando salga de la pantalla
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return; // Salir si ya impact�

        if (collision.CompareTag("Player"))
        {
            hasHit = true; // Marcar que ya ha impactado

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f); // Ajusta el da�o aqu�
            }

            // Reproducir el sonido de la explosi�n antes de marcar el impacto
            if (audioSource != null && explosionSound != null)
            {
                audioSource.volume = explosionVolume; // Establecer el volumen de la explosi�n
                audioSource.PlayOneShot(explosionSound); // Reproducir el sonido de explosi�n
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Destruir la bala despu�s de un peque�o retraso para que la animaci�n y el sonido se reproduzcan
            Destroy(gameObject, 0.5f); // Destruir la bala despu�s de un retraso para asegurar que el sonido se reproduzca
        }
    }


}
