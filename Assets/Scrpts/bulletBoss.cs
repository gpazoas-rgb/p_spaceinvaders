using UnityEngine;

public class bulletBoss : MonoBehaviour
{
    // 1. Referencias (Configurar en el Inspector)
    public GameObject bulletPrefab; // Prefab de la bala enemiga (BulletEnemiga)
    public Transform firePoint;     // Punto desde donde saldrá la bala (puede ser un objeto vacío hijo del jefe)

    // 2. Variables de Disparo
    public float timeBetweenShots = 1.0f; // Tiempo de espera entre disparos (ej: 1 segundo)
    private float nextFireTime;             // Momento en que puede volver a disparar

    void Start()
    {
        // Inicializa el primer disparo para que ocurra después de un pequeño retraso
        nextFireTime = Time.time + timeBetweenShots;
    }

    void Update()
    {
        // Solo dispara si el juego NO está pausado (Time.timeScale > 0)
        // Y si el tiempo actual es mayor que el tiempo del siguiente disparo
        if (Time.timeScale > 0 && Time.time > nextFireTime)
        {
            Shoot();

            // Establece el tiempo del próximo disparo
            nextFireTime = Time.time + timeBetweenShots;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Instancia la bala enemiga en la posición y rotación del punto de disparo
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Faltan referencias en BossShooting para disparar.");
        }
    }
}

