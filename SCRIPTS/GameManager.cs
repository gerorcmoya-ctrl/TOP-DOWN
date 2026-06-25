using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool juegoTerminado { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void TriggerGameOver()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        UIManager.Instance.MostrarGameOver();
        Time.timeScale = 0f;
    }

    public void TriggerVictoria()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        UIManager.Instance.MostrarVictoria();
        Time.timeScale = 0f;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}