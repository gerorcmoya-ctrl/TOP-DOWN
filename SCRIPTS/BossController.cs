using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] float velocidad = 2f;

    [Header("Disparo")]
    [SerializeField] GameObject balaPrefab;
    [SerializeField] float cadencia = 1.5f;
    [SerializeField] float cadenciaFase2 = 0.6f;

    [Header("Fases")]
    [SerializeField] int vidaFase2 = 150;

    bool enFase2 = false;
    Transform jugador;
    Rigidbody rb;
    Salud salud;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        salud = GetComponent<Salud>();
        jugador = GameObject.FindWithTag("Player")?.transform;
    }

    void Start()
    {
        StartCoroutine(PatronDisparo());
        salud.alMorir.AddListener(AlMorir);
        UIManager.Instance?.MostrarBarraBoss(salud.vidaMaxima);

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject zombie in zombies)
        {
            if (zombie != gameObject)
            {
                Physics.IgnoreCollision(
                    GetComponent<Collider>(),
                    zombie.GetComponent<Collider>()
                );
            }
        }
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0f;

        rb.linearVelocity = new Vector3(
            direccion.x * velocidad,
            rb.linearVelocity.y,
            direccion.z * velocidad
        );

        if (direccion != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direccion);

        if (!enFase2 && salud.vidaActual <= vidaFase2)
        {
            enFase2 = true;
            EntrarFase2();
        }
    }

    void Update()
    {
        UIManager.Instance?.ActualizarBarraBoss(salud.vidaActual);
    }

    IEnumerator PatronDisparo()
    {
        while (true)
        {
            yield return new WaitForSeconds(enFase2 ? cadenciaFase2 : cadencia);
            if (jugador == null) yield break;
            DispararRadial(enFase2 ? 8 : 4);
        }
    }

    void DispararRadial(int cantidadBalas)
    {
        if (jugador == null) return;

        Vector3 dirHaciaJugador = (jugador.position - transform.position).normalized;
        dirHaciaJugador.y = 0f;

        for (int i = 0; i < cantidadBalas; i++)
        {
            float angulo = i * (360f / cantidadBalas);
            Vector3 dir = Quaternion.Euler(0, angulo, 0) * dirHaciaJugador;

            GameObject bala = Instantiate(balaPrefab, transform.position, Quaternion.identity);
            bala.GetComponent<Bala>().SetDireccion(dir);

            Physics.IgnoreCollision(
                bala.GetComponent<Collider>(),
                GetComponent<Collider>()
            );

            Destroy(bala, 4f);
        }
    }

    void EntrarFase2()
    {
        velocidad *= 1.5f;
        Debug.Log("BOSS FASE 2!");
    }

    void AlMorir()
    {
        UIManager.Instance?.OcultarBarraBoss();
        GameManager.Instance?.TriggerVictoria();
    }
}