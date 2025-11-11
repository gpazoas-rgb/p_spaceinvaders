using UnityEngine;

public class Boss : MonoBehaviour
{
    // --- Configuración de Movimiento ---
    public float speed = 2.0f;     // Velocidad de movimiento horizontal
    public float limitX = 4.5f;    // Límite en el eje X para cambiar de dirección
    private int direction = 1;     // 1 = Derecha, -1 = Izquierda

    // >>> CAMBIO: Vida fijada a 3 para que muera al tercer golpe <<<
    public int health = 3;
    public GameObject bossExplosionPrefab;
    // **********************************

    // --- Configuración de Disparo ---
    public GameObject bossBulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 1.0f;
    private float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time;
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            HandleMovement();
            HandleShooting();
        }
    }

    void HandleMovement()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= limitX)
        {
            direction = -1;
        }
        else if (transform.position.x <= -limitX)
        {
            direction = 1;
        }
    }

    void HandleShooting()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + timeBetweenShots;
        }
    }

    void Shoot()
    {
        if (bossBulletPrefab != null && firePoint != null)
        {
            Instantiate(bossBulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Faltan referencias en BossController para el disparo.");
        }
    }

    // Función llamada por Bullet.cs (que le resta 1 punto de daño)
    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Jefe golpeado. Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (bossExplosionPrefab != null)
        {
            Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        Debug.Log("Jefe Destruido!");
    }
}