using UnityEngine;

public class ExplosicionEnemigo : MonoBehaviour
{
    // Duración en segundos. Ajusta esto para que coincida con tu animación.
    public float duration = 0.5f;

    void Start()
    {
        // Destruye el objeto (la explosión) después del tiempo de duración.
        Destroy(gameObject, duration);
    }
}

