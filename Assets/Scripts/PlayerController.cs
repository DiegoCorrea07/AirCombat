using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador
    public GameObject bulletPrefab; // Prefab de la bala
    public float bulletSpeed = 10f; // Velocidad de la bala
    public Transform arrow; // Referencia a la flecha (a��delo en el Inspector)
    public Transform shootPoint; // Referencia al punto de disparo (a��delo en el Inspector)

    private Animator animator; // Para controlar las animaciones
    private Vector2 screenBounds; // L�mites de la pantalla en coordenadas del mundo
    private float playerRadius; // Radio del jugador (usamos la flecha para calcularlo)

    void Start()
    {
        // Obt�n el componente Animator
        animator = GetComponent<Animator>();

        // Calcula los l�mites de la pantalla en coordenadas del mundo
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Obtener el radio del jugador basado en la flecha (la circunferencia que lo rodea)
        if (arrow != null)
        {
            // El radio se calcula como la mitad de la distancia del tama�o de la flecha
            playerRadius = arrow.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
    }

    void LimitPlayerMovement()
    {
        // Limitar la posici�n X del jugador (esto ya est� funcionando bien)
        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + playerRadius, screenBounds.x - playerRadius);

        // Limitar la posici�n Y del jugador
        // Usamos el radio para asegurar que el jugador no pase los bordes de la pantalla.
        float clampedY = Mathf.Clamp(transform.position.y, -screenBounds.y + playerRadius, screenBounds.y - playerRadius);

        // Aseg�rate de que el jugador se quede en la posici�n Z correcta (en 2D Z = 0)
        transform.position = new Vector3(clampedX, clampedY, 0f);
    }

    void Update()
    {
        // Movimiento del jugador con WASD
        float horizontal = Input.GetAxisRaw("Horizontal"); // A y D
        float vertical = Input.GetAxisRaw("Vertical");     // W y S

        // Aplicar movimiento
        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized * speed * Time.deltaTime;
        transform.Translate(movement);

        // Limitar la posici�n del jugador dentro de los l�mites de la pantalla
        LimitPlayerMovement();

        // Disparo al presionar la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Crear la bala en la posici�n del punto de disparo (ShootPoint)
        Vector3 bulletPosition = shootPoint.position;
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

        // Obtener la direcci�n de disparo a partir de la rotaci�n de la flecha
        float angle = arrow.rotation.eulerAngles.z; // Obtiene el �ngulo Z de la flecha
        Vector2 shootDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

        // Aplicar velocidad a la bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootDirection * bulletSpeed; // Mover la bala en la direcci�n de la flecha
        }

        // **Rotar la bala hacia la direcci�n del disparo**
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Destruir la bala tras el impacto
        }
    }
}
