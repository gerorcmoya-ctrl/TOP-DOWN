using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] float radioDeteccion = 1.5f;
    Transform jugador;

    void Start()
    {
        jugador = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);
        if (distancia <= radioDeteccion)
        {
            AudioManager.Instance?.PlayPowerUp();
            Aplicar(jugador.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void Aplicar(GameObject jugador);
}