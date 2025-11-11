using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    // 1. Singleton para acceso global
    public static UIManager Instance;

    // 2. REFERENCIAS DE UI (Arrastrar desde el Inspector)
    public GameObject startPanel;    // Panel de Menú de Inicio
    public GameObject gameOverPanel; // Panel de Game Over
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalMessageText; // Texto dinámico (VICTORIA/GAME OVER)

    // 3. VARIABLES DE JUEGO
    private int score = 0;
    public int scorePerEnemy = 10;

    // Asumimos 9 enemigos (3x3) para la condición de Victoria
    public int totalEnemies;

    // Variables de cronómetro
    public float maxTime = 60f;
    private float currentTime;
    private bool isTimerRunning = false; // El tiempo está detenido al inicio

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTime = maxTime;
        UpdateScoreDisplay();
        UpdateTimeDisplay();

        // -----------------------------------------------------
        // >>> CONFIGURACIÓN DE INICIO: PAUSA Y MENÚ <<<

        // Asumimos 9 enemigos para iniciar el contador de victoria
        totalEnemies = 9;

        // Oculta el Game Over y muestra el menú de Inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        // Pausa la simulación del juego y el cronómetro
        Time.timeScale = 0f;
        isTimerRunning = false;
        // -----------------------------------------------------
    }

    void Update()
    {
        if (isTimerRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                GameOver("GAME OVER"); // Pierde por tiempo
            }
            UpdateTimeDisplay();
        }
    }

    // --- MÉTODOS PÚBLICOS ---

    public void AddScore()
    {
        score += scorePerEnemy;
        UpdateScoreDisplay();
    }

    // Función llamada por el botón "COMENZAR"
    public void StartGame()
    {
        // 1. Oculta el panel de inicio
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        // 2. Reanuda el juego
        Time.timeScale = 1f;
        isTimerRunning = true;

        Debug.Log("Juego Iniciado.");
    }

    // Llamado por Bullet.cs cuando un enemigo es destruido
    public void EnemyDestroyed()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
        {
            GameOver("VICTORIA"); // Gana al eliminar todos los enemigos
        }
    }

    // Llamado por Nave.cs cuando pierde una vida
    public void UpdateLives(int currentLives)
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + currentLives;
        }

        if (currentLives <= 0)
        {
            GameOver("GAME OVER"); // Pierde por vidas
        }
    }

    // Función principal de fin de juego
    public void GameOver(string message)
    {
        if (Time.timeScale == 0) return; // Ya está pausado

        isTimerRunning = false;

        if (finalMessageText != null)
        {
            finalMessageText.text = message;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Debug.Log("Juego Terminado: " + message);
    }

    // --- MÉTODOS DE VISUALIZACIÓN ---

    public void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }

    private void UpdateTimeDisplay()
    {
        if (timerText != null)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.text = "Tiempo: " + time.ToString(@"mm\:ss");
        }
    }
}