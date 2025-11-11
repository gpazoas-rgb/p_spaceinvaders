using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Velocidad de la bala, ajustable en el Inspector
    public float speed = 15f;

    // Referencia al Prefab de explosión del enemigo
    public GameObject enemyExplosionPrefab;

    void Update()
    {
        // 1. Mueve la bala hacia arriba constantemente
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 2. Destruye la bala si sale de la pantalla (limpieza de memoria)
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // -----------------------------------------------------
        // >>> LÓGICA DE DAÑO DEL JEFE <<<
        // Verifica si el objeto golpeado tiene el script BossController
        Boss bossController = other.GetComponent<Boss>();

        if (bossController != null)
        {
            // 1. Llama a la función de daño del jefe (le quita 1 punto de vida)
            bossController.TakeDamage(1);

            // 2. Destruye solo la bala, no el jefe
            Destroy(gameObject);

            // IMPORTANTE: Retorna para no ejecutar la lógica del enemigo normal
            return;
        }
        // -----------------------------------------------------

        // --- 1. DETECCIÓN DE ENEMIGO NORMAL ("Enemigo" tag) ---
        if (other.CompareTag("Enemigo"))
        {
            // AÑADIR PUNTOS y reducir contador de enemigos
            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddScore();
                UIManager.Instance.EnemyDestroyed();
            }

            // INSTANCIAR LA EXPLOSIÓN
            if (enemyExplosionPrefab != null)
            {
                Instantiate(enemyExplosionPrefab, other.transform.position, Quaternion.identity);
            }

            // Destruir objetos
            Destroy(other.gameObject); // Destruye el Enemigo
            Destroy(gameObject);       // Destruye la Bala (del jugador)
        }

        // (El búnker usa OnCollisionEnter2D, por lo que no interfiere aquí)
    }
}