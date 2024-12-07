using UnityEngine;

public class BulletControllerEnemy : MonoBehaviour
{
    public float speed = 10f; // Velocidad constante de la bala
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    //public Animator bulletAnimator; // Referencia al Animator de la bala
    public AudioClip shootSound; // Sonido de disparo
    public AudioClip explosionSound; // Sonido de explosión
    private AudioSource audioSource; // Componente de AudioSource
    public float shootVolume = 1f; // Control del volumen para el disparo
    public float explosionVolume = 1f; // Control del volumen para la explosión
    public GameObject explosionPrefab; // Prefab de explosión

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

        // El movimiento de la bala será controlado por el Rigidbody2D
        // Este script no maneja la velocidad directamente, ya que lo puedes manejar desde otro lado (Enemy o Player)
    }

    void Update()
    {
        // Este método ahora está vacío porque el movimiento de la bala ya depende del Rigidbody2D.
    }

    private void OnBecameInvisible()
    {
        // Destruir la bala cuando salga de la pantalla
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return; // Salir si ya impactó

        if (collision.CompareTag("Player"))
        {
            hasHit = true; // Marcar que ya ha impactado

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f); // Ajusta el daño aquí
            }

            // Reproducir el sonido de la explosión antes de marcar el impacto
            if (audioSource != null && explosionSound != null)
            {
                audioSource.volume = explosionVolume; // Establecer el volumen de la explosión
                audioSource.PlayOneShot(explosionSound); // Reproducir el sonido de explosión
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Destruir la bala después de un pequeño retraso para que la animación y el sonido se reproduzcan
            Destroy(gameObject, 0.5f); // Destruir la bala después de un retraso para asegurar que el sonido se reproduzca
        }
    }


}
