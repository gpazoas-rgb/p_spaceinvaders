using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int hitsToDestroy = 4;
    private int currentHits;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHits = hitsToDestroy;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("Bunker no tiene SpriteRenderer.");
        }
    }

    private void TakeDamage()
    {
        currentHits--;

        if (currentHits <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateAppearance();
        }
    }

    void UpdateAppearance()
    {
        if (spriteRenderer != null)
        {
            float healthRatio = (float)currentHits / hitsToDestroy;

            // Usa el canal Alpha (transparencia) para mostrar daño
            Color color = spriteRenderer.color;
            color.a = healthRatio;
            spriteRenderer.color = color;
        }
    }

    // Detecta la colisión SÓLIDA.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // El Debug.Log es útil para confirmar la colisión si tienes dudas
        // Debug.Log("BUNKER: Colisión detectada con " + collision.gameObject.tag); 

        // Si la colisión es con una bala (del jugador o enemiga)
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("BulletEnemiga"))
        {
            TakeDamage();

            // Destruye la bala que chocó
            Destroy(collision.gameObject);
        }
    }
}