using UnityEngine;

public class Enemigos : MonoBehaviour
{
    // ¡ESTA ES LA LÍNEA CRUCIAL QUE DEBE ESTAR AHÍ!
    public GameObject enemyPrefab;

    // Dimensiones de la cuadrícula
    public int columns = 4; // Horizontal
    public int rows = 3;    // Vertical

    // Espaciado entre enemigos
    public float spacingX = 1.5f;
    public float spacingY = 1.0f;
    void Start()
    {
        Debug.Log("El Spawner ha iniciado. Intentando generar cuadrícula.");
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Céntra el punto de inicio de la cuadrícula
        float startX = -(columns - 1) * spacingX / 2;
        float startY = 0f;

        for (int i = 0; i < columns; i++) // Columnas (Horizontal)
        {
            for (int j = 0; j < rows; j++) // Filas (Vertical)
            {
                // Calcula la posición relativa
                float xPos = startX + i * spacingX;
                float yPos = startY - j * spacingY;

                Vector3 relativePosition = new Vector3(xPos, yPos, 0);

                // Crea el enemigo. ¡Esta es la línea clave!
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position + relativePosition, Quaternion.identity);

                // Organiza la jerarquía (Opcional, pero recomendado)
                newEnemy.transform.SetParent(transform);
            }
        }
    }
}
