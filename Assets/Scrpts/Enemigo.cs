using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Variables de Movimiento
    public float speed = 1f;
    public float descentAmount = 0.3f;
    public float leftBoundary = -8f;
    public float rightBoundary = 8f;
    private static int direction = 1;

    // Variables para el DISPARO
    public GameObject enemyBulletPrefab;
    public float fireRate = 3f;
    private float nextFireTime;

    // ¡NUEVO! Variable para la Explosión
    public GameObject explosionPrefab;

    void Start()
    {
        // Inicializa el primer tiempo de disparo de forma aleatoria
        nextFireTime = Time.time + Random.Range(fireRate * 0.5f, fireRate * 1.5f);
    }

    void Update()
    {
        // Lógica de Movimiento
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= rightBoundary && direction == 1)
        {
            ChangeDirection();
        }
        else if (transform.position.x <= leftBoundary && direction == -1)
        {
            ChangeDirection();
        }

        // Lógica de Disparo del Enemigo
        if (Time.time > nextFireTime)
        {
            Shoot();
            // Calcula el siguiente disparo con una pequeña variación
            nextFireTime = Time.time + fireRate + Random.Range(-0.5f, 0.5f);
        }
    }

    // Nueva función para disparar la bala
    void Shoot()
    {
        // Instancia la bala en la posición del enemigo
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
    }

    void ChangeDirection()
    {
        direction *= -1;
        transform.Translate(Vector3.down * descentAmount); // Desciende al cambiar de dirección
        speed += 0.05f;
    }

    // ¡NUEVA FUNCIÓN! Maneja la colisión con la bala del jugador.
    void OnTriggerEnter2D(Collider2D other)
    {
        // La bala del jugador debe tener el Tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // 1. Instanciar la explosión
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // 2. Sumar puntos (Llama al Singleton UIManager)
            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddScore();
            }

            // 3. Destruir los objetos
            Destroy(other.gameObject); // Destruye la bala del jugador
            Destroy(gameObject);      // Destruye al enemigo
        }
    }
}