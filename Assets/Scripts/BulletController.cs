using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f; // Velocidad constante de la bala
    public Animator bulletAnimator; // Referencia al Animator de la bala
    public GameObject explosionPrefab; // Prefab de explosión
    public GameObject smokePrefab; // Prefab de humo cuando la bala es disparada
    public GameObject movingSmokePrefab; // Prefab de humo mientras la bala se mueve
    public AudioClip shootSound; // Sonido de disparo
    public AudioClip explosionSound; // Sonido de explosión
    private AudioSource audioSource; // Componente de AudioSource
    public float shootVolume = 1f; // Control del volumen para el disparo
    public float explosionVolume = 1f; // Control del volumen para la explosión

    private GameObject smokeEffect; // Referencia al efecto de humo al disparar
    private GameObject movingSmokeEffect; // Referencia al efecto de humo mientras se mueve

    void Start()
    {
        // Inicializar el AudioSource
        audioSource = GetComponent<AudioSource>();

        // Asegurarse de que no se reproduzca el sonido automáticamente al inicio
        audioSource.playOnAwake = false;

        // Reproducir el sonido de disparo solo cuando se crea el proyectil
        if (audioSource != null && shootSound != null)
        {
            audioSource.volume = shootVolume; // Establecer el volumen del disparo
            audioSource.PlayOneShot(shootSound); // Reproducir el sonido de disparo
        }

        // Instanciar el humo cuando la bala es disparada
        if (smokePrefab != null)
        {
            smokeEffect = Instantiate(smokePrefab, transform.position, Quaternion.identity);
            Destroy(smokeEffect, 1f); // Destruir el humo después de un corto tiempo
        }

        // Instanciar el humo mientras la bala se mueve
        if (movingSmokePrefab != null)
        {
            movingSmokeEffect = Instantiate(movingSmokePrefab, transform.position, Quaternion.identity);
            movingSmokeEffect.transform.SetParent(transform); // Hacer que el humo se mueva con la bala
        }

        // Opcional: Ajusta la rotación si es necesario para la orientación visual de la bala.
        transform.rotation = Quaternion.Euler(0, 0, 180); // Esto depende del diseño visual del prefab.
    }

    void Update()
    {
        // Mover la bala hacia la izquierda en el eje horizontal
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        // Destruir la bala cuando salga de la pantalla
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si la bala colisionó con un enemigo
        if (collision.CompareTag("Enemy"))
        {
            // Llamar al método de TakeHit del enemigo (no destruir al enemigo)
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeHit(); // Registrar el impacto en el enemigo
            }

            // Activar la animación de explosión solo cuando la bala colisione con un enemigo
            if (bulletAnimator != null)
            {
                bulletAnimator.SetTrigger("ExplosionTrigger");
            }

            // Instanciar la explosión (si tienes un prefab de explosión)
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Reproducir el sonido de la explosión solo cuando colisiona con un enemigo
            if (audioSource != null && explosionSound != null)
            {
                audioSource.volume = explosionVolume; // Establecer el volumen de la explosión
                audioSource.PlayOneShot(explosionSound); // Reproducir el sonido de explosión
            }

            // Destruir la bala después de la animación y sonido
            Destroy(gameObject, 0.5f); // Destruye la bala después de un pequeño retraso para que la animación y el sonido se reproduzcan
        }
    }

    // Método para detener el humo de movimiento cuando la bala colisiona
    private void StopMovingSmoke()
    {
        if (movingSmokeEffect != null)
        {
            Destroy(movingSmokeEffect); // Destruir el humo cuando ya no sea necesario
        }
    }
}
