using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemigoPrefab;
    [SerializeField] Transform[] puntosSpawn;
    [SerializeField] int enemigosporOleada = 3;
    [SerializeField] float tiempoEntreOleadas = 5f;
    [SerializeField] int oleadasParaGanar = 5;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform spawnBoss;

    public int oleadaActual = 0;
    int enemigosVivos = 0;

    void Start() => StartCoroutine(SpawnLoop());

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            oleadaActual++;
            UIManager.Instance?.ActualizarOleada(oleadaActual);

            if (oleadaActual > oleadasParaGanar)
            {
                if (bossPrefab != null)
                {
                    GameObject[] zombiesVivos = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject z in zombiesVivos)
                        Destroy(z);

                    enemigosVivos = 0;
                    Instantiate(bossPrefab, spawnBoss.position, Quaternion.identity);
                    UIManager.Instance?.ActualizarOleada(99);
                }
                else
                {
                    GameManager.Instance?.TriggerVictoria();
                }
                yield break;
            }

            int cantidad = enemigosporOleada + (oleadaActual - 1) * 2;

            for (int i = 0; i < cantidad; i++)
            {
                Transform punto = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
                GameObject e = Instantiate(enemigoPrefab, punto.position, Quaternion.identity);
                enemigosVivos++;

                Salud salud = e.GetComponent<Salud>();
                salud.alMorir.AddListener(() => enemigosVivos--);

                yield return new WaitForSeconds(0.8f);
            }

            yield return new WaitUntil(() => enemigosVivos <= 0);
            yield return new WaitForSeconds(tiempoEntreOleadas);
        }
    }
}