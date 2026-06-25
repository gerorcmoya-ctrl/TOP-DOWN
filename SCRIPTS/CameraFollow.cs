using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform objetivo;
    [SerializeField] float suavidad = 5f;
    Vector3 offset;

    void Awake()
        => offset = transform.position - objetivo.position;

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 posicionDeseada = objetivo.position + offset;
        transform.position = Vector3.Lerp(
            transform.position, posicionDeseada, suavidad * Time.deltaTime);
    }
}