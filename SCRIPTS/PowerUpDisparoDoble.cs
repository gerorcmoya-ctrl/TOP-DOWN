using UnityEngine;
using System.Collections;

public class PowerUpDisparoDoble : PowerUp
{
    [SerializeField] float duracion = 8f;

    protected override void Aplicar(GameObject jugador)
    {
        PlayerShoot shoot = jugador.GetComponent<PlayerShoot>();
        if (shoot == null) return;
        shoot.StartCoroutine(ActivarDoble(shoot));
    }

    IEnumerator ActivarDoble(PlayerShoot shoot)
    {
        shoot.disparaDoble = true;
        yield return new WaitForSeconds(duracion);
        shoot.disparaDoble = false;
    }
}