using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game settings")]
    [SerializeField] private int startingLives = 3;

    [Header("UI (Phase 7)")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameObject gameOverPanel;

    private int score;
    private int lives;

    // fem això perquè els altres scripts només o puguin llegir però no modificar aquest valor.
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        // TODO: Singleton (igual que amb les pools).
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        // TODO: inicialitza el joc.
        //   score = 0, lives = startingLives, IsGameOver = false
        score = 0; 
        lives = startingLives; 
        IsGameOver = false;
        //   amaga el panell GameOver
        gameOverPanel.gameObject.SetActive(false);
        //   Crida UpdateUI().
        UpdateUI();
    }

    // Cridat pel Asteroid quan aquest és destruït.
    public void AddScore(int points)
    {
        // TODO: afegeix punts a la UI i actualitza-la.
        score += points;
        scoreText.text = scoreText.text + " " + score;
    }

    // Cridat pel Ateroid quan impacta amb el player.
    public void LoseLife(int amount)
    {
        // TODO: Resta amount a lives i actualitza la UI.
        lives -= amount;
        livesText.text = amount.ToString();
        //       si lives < 0 GameOver().
        if (lives < 0) 
        { 
            GameOver(); 
        }
        //       Extra: si el joc ja ha terminat no facis res
        if (IsGameOver) return;
    }

    private void GameOver()
    {
        // TODO:
        //     IsGameOver = true
        IsGameOver = true;
        //     Fes aparèixer el panell de GameOver
        gameOverPanel.gameObject.SetActive(true);
        //     Congela el joc Time.timeScale = 0
        Time.timeScale = 0;
    }

    // Assigna aquest mètode al Restart button: On Click () al Inspector.
    public void RestartGame()
    {
        // TODO:
        //     Time.timeScale = 1   (si no fas això reiniciarà congelat)
        Time.timeScale = 1;
        //     fes un reload de l'escena:
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateUI()
    {
        // TODO: escriu les vides i els punts als textos.
        //   Exemple: scoreText.text = "Punts: " + score;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
}
