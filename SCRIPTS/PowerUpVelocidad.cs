using UnityEngine;
using System.Collections;

public class PowerUpVelocidad : PowerUp
{
    [SerializeField] float velocidadBoost = 15f;
    [SerializeField] float duracion = 5f;

    protected override void Aplicar(GameObject jugador)
    {
        PlayerMovement mov = jugador.GetComponent<PlayerMovement>();
        if (mov == null)
            mov = jugador.GetComponentInChildren<PlayerMovement>();
        if (mov == null)
            mov = jugador.GetComponentInParent<PlayerMovement>();

        if (mov == null)
        {
            Debug.Log("No encontró PlayerMovement");
            return;
        }

        Debug.Log("PowerUp velocidad aplicado!");
        mov.StartCoroutine(BoostVelocidad(mov));
    }

    IEnumerator BoostVelocidad(PlayerMovement mov)
    {
        float velocidadOriginal = mov.velocidad;
        mov.velocidad = velocidadBoost;
        yield return new WaitForSeconds(duracion);
        mov.velocidad = velocidadOriginal;
    }
}