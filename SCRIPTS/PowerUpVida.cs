using UnityEngine;

public class PowerUpVida : PowerUp
{
    [SerializeField] int cantidadCuracion = 25;

    protected override void Aplicar(GameObject jugador)
    {
        Salud salud = jugador.GetComponent<Salud>();
        if (salud == null) return;

        salud.vidaActual += cantidadCuracion;
        if (salud.vidaActual > salud.vidaMaxima)
            salud.vidaActual = salud.vidaMaxima;

        UIManager.Instance?.ActualizarVida(salud.vidaActual, salud.vidaMaxima);
    }
}