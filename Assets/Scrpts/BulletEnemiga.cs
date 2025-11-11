using UnityEngine;

public class BulletEnemiga : MonoBehaviour
{
    public float speed = 8f; // Velocidad de la bala enemiga

    void Update()
    {
        // Mueve la bala hacia ABAJO (Vector3.down)
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destruye la bala si sale por la parte inferior de la pantalla
        // Nota: El límite puede necesitar ajustarse si tu escena es más grande/pequeña.
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Nave"))
        {
            // NO DEBE ESTAR DESTROY(OTHER.GAMEOBJECT);
            // Debe quedar SOLO:
            Destroy(gameObject); // Destruye SOLAMENTE la bala.
        }
    }
}