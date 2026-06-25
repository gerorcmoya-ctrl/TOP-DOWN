using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Jugador")]
    [SerializeField] Slider vidaSlider;
    [SerializeField] TextMeshProUGUI textoOleada;

    [Header("Paneles")]
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelVictoria;

    [Header("Boss")]
    [SerializeField] Slider bossHealthBar;
    [SerializeField] GameObject textoBoss;

    Coroutine coroutineVida;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        Salud saludJugador = GameObject.FindWithTag("Player")?.GetComponent<Salud>();
        if (saludJugador != null)
            vidaSlider.value = saludJugador.vidaActual;
    }

    public void ActualizarVida(int actual, int maximo)
    {
        vidaSlider.maxValue = maximo;
        if (coroutineVida != null) StopCoroutine(coroutineVida);
        coroutineVida = StartCoroutine(AnimarBarra(actual));
    }

    IEnumerator AnimarBarra(float objetivo)
    {
        float velocidad = 5f;
        while (Mathf.Abs(vidaSlider.value - objetivo) > 0.1f)
        {
            vidaSlider.value = Mathf.Lerp(vidaSlider.value, objetivo, velocidad * Time.deltaTime);
            yield return null;
        }
        vidaSlider.value = objetivo;
    }

    public void ActualizarOleada(int oleada)
        => textoOleada.text = "Oleada: " + oleada;

    public void MostrarGameOver() => panelGameOver.SetActive(true);
    public void MostrarVictoria() => panelVictoria.SetActive(true);

    public void MostrarBarraBoss(int vidaMax)
    {
        bossHealthBar.maxValue = vidaMax;
        bossHealthBar.value = vidaMax;
        bossHealthBar.gameObject.SetActive(true);
        textoBoss.SetActive(true);
    }

    public void ActualizarBarraBoss(int vidaActual)
    {
        if (bossHealthBar.gameObject.activeSelf)
            StartCoroutine(AnimarBarraBoss(vidaActual));
    }

    IEnumerator AnimarBarraBoss(float objetivo)
    {
        float velocidad = 5f;
        while (Mathf.Abs(bossHealthBar.value - objetivo) > 0.1f)
        {
            bossHealthBar.value = Mathf.Lerp(bossHealthBar.value, objetivo, velocidad * Time.deltaTime);
            yield return null;
        }
        bossHealthBar.value = objetivo;
    }

    public void OcultarBarraBoss()
    {
        bossHealthBar.gameObject.SetActive(false);
        textoBoss.SetActive(false);
    }
}