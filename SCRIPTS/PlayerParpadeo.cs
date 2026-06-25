using System.Collections;
using UnityEngine;

public class PlayerParpadeo : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] Color colorDanio = Color.red;
    [SerializeField] float duracion = 0.1f;
    [SerializeField] int cantidadParpadeos = 3;

    Material[] materialesOriginales;
    Material materialRojo;

    void Awake()
    {
        // Guardar materiales originales
        materialesOriginales = meshRenderer.materials;

        // Crear material rojo
        materialRojo = new Material(meshRenderer.materials[0]);
        materialRojo.color = colorDanio;
    }

    public void Parpadear()
    {
        StopAllCoroutines();
        StartCoroutine(EfectoParpadeo());
    }

    IEnumerator EfectoParpadeo()
    {
        Material[] materialesRojos = new Material[materialesOriginales.Length];
        for (int i = 0; i < materialesRojos.Length; i++)
            materialesRojos[i] = materialRojo;

        for (int i = 0; i < cantidadParpadeos; i++)
        {
            meshRenderer.materials = materialesRojos;
            yield return new WaitForSeconds(duracion);
            meshRenderer.materials = materialesOriginales;
            yield return new WaitForSeconds(duracion);
        }
    }
}