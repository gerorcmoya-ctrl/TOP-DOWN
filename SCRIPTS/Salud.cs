using UnityEngine;
using UnityEngine.Events;
using System;

public class Salud : MonoBehaviour
{
    [SerializeField] public int vidaMaxima = 50;
    [NonSerialized] public int vidaActual;
    public UnityEvent alMorir;
    [SerializeField] GameObject particulasMuerte;
    bool muertoYa = false;

    void Awake()
    {
        muertoYa = false;
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(int cantidad)
    {
        if (muertoYa) return;
        vidaActual -= cantidad;

        if (CompareTag("Player"))
        {
            UIManager.Instance?.ActualizarVida(vidaActual, vidaMaxima);
            GetComponent<PlayerParpadeo>()?.Parpadear();
        }

        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        if (muertoYa) return;
        muertoYa = true;

        Debug.Log("Murió: " + gameObject.name + " vida: " + vidaActual);

        if (particulasMuerte != null)
            Instantiate(particulasMuerte, transform.position, Quaternion.identity);

        alMorir?.Invoke();

        if (CompareTag("Player"))
            GameManager.Instance?.TriggerGameOver();

        Destroy(gameObject);
    }
}