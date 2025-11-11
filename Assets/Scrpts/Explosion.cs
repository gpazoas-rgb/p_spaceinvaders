using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 0.5f; // Duración en segundos de la explosión

    void Start()
    {
        // Destruye el objeto de explosión después de 'duration' segundos
        Destroy(gameObject, duration);
    }
}
