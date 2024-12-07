using UnityEngine;

public class BulletControllerEnemy : MonoBehaviour
{
    public float speed = 10f; // Velocidad constante de la bala
    private Rigidbody2D rb; // Referencia al Rigidbody2D
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
    }

    void Update()
    {
        // El movimiento de la bala ya depende del Rigidbody2D.
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

            // Reproducir el sonido de la explosi�n antes de destruir la bala
            if (audioSource != null && explosionSound != null)
            {
                audioSource.volume = explosionVolume; // Ajustar el volumen de la explosi�n
                audioSource.PlayOneShot(explosionSound); // Reproducir el sonido de la explosi�n
            }

            // Instanciar el prefab de explosi�n, si est� configurado
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Destruir la bala **inmediatamente despu�s de reproducir el sonido y la animaci�n**
            Destroy(gameObject); // La bala se destruye inmediatamente, sin retraso.
        }
    }
}
