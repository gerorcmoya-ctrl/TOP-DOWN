using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] float chanceDrop = 0.3f; // 30% de probabilidad
    [SerializeField] float velocidad = 3f;
    [SerializeField] float rangoAtaque = 1.5f;
    [SerializeField] float cadenciaAtaque = 1f;
    [SerializeField] int danio = 10;
    Transform jugador;
    Rigidbody rb;
    float proximoAtaque;
    void Start()
    {
        GetComponent<Salud>().alMorir.AddListener(DropPowerUp);
    }

    void DropPowerUp()
    {
        if (powerUpPrefabs.Length == 0) return;
        if (Random.value > chanceDrop) return;

        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Vector3 posicion = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Instantiate(prefab, posicion, Quaternion.identity);
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jugador = GameObject.FindWithTag("Player")?.transform;
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);
        Vector3 direccion = (jugador.position - transform.position).normalized;

        // Rotar hacia el jugador
        direccion.y = 0f;
        if (direccion != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direccion);

        if (distancia > rangoAtaque)
        {
            // Perseguir
            rb.linearVelocity = new Vector3(
                direccion.x * velocidad,
                rb.linearVelocity.y,
                direccion.z * velocidad
            );
        }
        else
        {
            // Atacar
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            if (Time.time >= proximoAtaque)
            {
                proximoAtaque = Time.time + cadenciaAtaque;
                // Solo atacar al jugador, no al boss
                if (jugador.CompareTag("Player"))
                    jugador.GetComponentInParent<Salud>()?.RecibirDanio(danio);
            }
        }
    }
}