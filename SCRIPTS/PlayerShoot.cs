using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public bool disparaDoble = false;
    [SerializeField] GameObject balaPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float cadencia = 0.2f;
    float proximoDisparo;
    Camera camara;
    Animator animator;
    void Awake()
    {
        camara = Camera.main;
        animator = GetComponent<Animator>(); // antes decía GetComponentInChildren
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= proximoDisparo)
        {
            proximoDisparo = Time.time + cadencia;
            Disparar();
        }
    }

    void Disparar()
    {
        animator.SetTrigger("golpear");
        AudioManager.Instance?.PlayDisparo();
        Ray rayo = camara.ScreenPointToRay(Input.mousePosition);
        Vector3 puntoDestino;

        if (Physics.Raycast(rayo, out RaycastHit hit, 100f))
            puntoDestino = hit.point;
        else
            puntoDestino = rayo.GetPoint(50f);

        Vector3 direccion = (puntoDestino - firePoint.position);
        direccion.y = 0f;

        SpawnBala(direccion);

        if (disparaDoble)
        {
            // Segunda bala con un poco de offset
            Vector3 direccionOffset = Quaternion.Euler(0, 15f, 0) * direccion;
            SpawnBala(direccionOffset);
        }
    }

    void SpawnBala(Vector3 direccion)
    {
        GameObject bala = Instantiate(balaPrefab, firePoint.position, Quaternion.identity);
        bala.GetComponent<Bala>().SetDireccion(direccion);
        Physics.IgnoreCollision(bala.GetComponent<Collider>(), GetComponent<Collider>());
        Destroy(bala, 3f);
    }
}