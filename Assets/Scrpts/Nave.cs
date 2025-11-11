using UnityEngine;
using UnityEngine.SceneManagement;

public class Nave : MonoBehaviour
{
    // Variables de movimiento
    public float speed = 7f;
    public float minX = -8f;
    public float maxX = 8f;

    // Variables para el DISPARO
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    // Variables para la DESTRUCCIÓN/VIDAS
    public GameObject explosionPrefab;
    public int lives = 3;
    private Vector3 initialPosition = new Vector3(0, -4, 0);

    void Start()
    {
        transform.position = initialPosition;
        lives = 3;

        // Actualiza el HUD con las vidas iniciales
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(lives);
        }

        Debug.Log("Juego iniciado. Vidas iniciales: " + lives);
    }

    void Update()
    {
        // 1. Lógica de Movimiento
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalInput * speed * Time.deltaTime;
        transform.Translate(movement);

        // 2. Lógica de Límites
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        transform.position = currentPosition;

        // 3. Lógica de Disparo
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    // ¡CAMBIO CLAVE! Usa OnCollisionEnter2D para detectar colisión sólida.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detección de impacto por bala enemiga
        if (collision.gameObject.CompareTag("BulletEnemiga"))
        {
            Debug.Log("Impacto detectado. Vidas antes: " + lives);

            // Destruir la bala enemiga 
            Destroy(collision.gameObject);

            // Restar una vida
            lives--;

            // Actualiza el HUD
            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateLives(lives);
            }

            if (lives > 0)
            {
                // REAPARECER
                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                }

                // Mover la nave a su posición inicial
                transform.position = initialPosition;

                Debug.Log("¡Reaparición! Vidas restantes: " + lives);
            }
            else
            {
                // GAME OVER - DESTRUCCIÓN FINAL
                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);

                // Llama al reinicio con RETRASO (2 segundos)
                Invoke("RestartGame", 2f);

                Debug.Log("GAME OVER. Juego reiniciado.");
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}