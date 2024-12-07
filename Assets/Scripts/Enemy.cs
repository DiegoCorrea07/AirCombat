using UnityEngine;
using UnityEngine.UI; // Necesario para usar Slider

public class Enemy : MonoBehaviour
{
    public enum MovementType { TowardsPlayer, Left }; // Tipos de movimiento
    public MovementType movementType; // Tipo de movimiento asignado

    public float speed = 3f; // Velocidad de movimiento
    public int maxHits = 2; // Impactos máximos antes de destruir al enemigo
    private int currentHits; // Impactos actuales

    public Transform playerTransform; // Referencia al jugador
    public GameObject bulletPrefab; // Prefab de la bala
    public float shootInterval = 2f; // Tiempo entre disparos
    public float bulletSpeed = 5f; // Velocidad de la bala
    public float detectionRange = 10f; // Rango de detección para disparar

    private float shootTimer; // Temporizador para el disparo

    // Referencia a la barra de vida
    public Slider healthBar;

    // Delegado para notificar cuando el enemigo es destruido
    public event System.Action<GameObject> OnDestroyed;

    void Start()
    {
        // Encuentra al jugador por su tag
        playerTransform = GameObject.FindWithTag("Player").transform;
        shootTimer = shootInterval; // Inicializar el temporizador de disparo

        // Inicializar impactos actuales y la barra de vida
        currentHits = maxHits;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHits;
            healthBar.value = maxHits; // Configurar barra de vida al máximo al inicio
        }
    }

    void Update()
    {
        Move(); // Ejecuta el movimiento según el tipo asignado
        HandleShooting(); // Gestionar el disparo
    }

    private void Move()
    {
        if (movementType == MovementType.TowardsPlayer && playerTransform != null)
        {
            // Movimiento hacia el jugador
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
        else if (movementType == MovementType.Left)
        {
            // Movimiento hacia la izquierda
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    private void HandleShooting()
    {
        if (playerTransform == null) return;

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Solo disparar si el jugador está dentro del rango
        if (distanceToPlayer <= detectionRange)
        {
            // Reducir el temporizador
            shootTimer -= Time.deltaTime;

            // Si el temporizador llega a 0, dispara
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootInterval; // Reiniciar el temporizador
            }
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null)
        {
            // Instanciar la bala en la posición del enemigo
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Calcular la dirección hacia el jugador
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Asignar velocidad a la bala
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = direction * bulletSpeed; // Mover la bala hacia el jugador
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruir al enemigo al chocar con el jugador
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeHit(); // Registrar el impacto
            Destroy(collision.gameObject); // Destruir la bala tras el impacto
        }
    }

    public void TakeHit()
    {
        currentHits--; // Reducir el número de impactos actuales

        // Actualizar barra de vida
        if (healthBar != null)
        {
            healthBar.value = currentHits;
        }

        if (currentHits <= 0)
        {
            // Aquí se invoca el evento que notifica que el enemigo ha sido destruido
            OnDestroyed?.Invoke(gameObject); // Pasando el GameObject del enemigo

            Destroy(gameObject); // Destruir al enemigo si se queda sin impactos
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar el rango de detección en la vista de escena para depuración
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
