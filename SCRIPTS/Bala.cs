using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] float velocidad = 15f;
    Vector3 direccion;

    public void SetDireccion(Vector3 dir)
        => direccion = dir.normalized;

    void Update()
        => transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        if (other.TryGetComponent<Salud>(out var salud))
            salud.RecibirDanio(10);

        Destroy(gameObject);
    }
}