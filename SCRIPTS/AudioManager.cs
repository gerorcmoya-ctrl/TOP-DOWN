using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Música")]
    [SerializeField] AudioSource musicaFondo;

    [Header("Efectos")]
    [SerializeField] AudioSource efectoDisparo;
    [SerializeField] AudioSource efectoPowerUp;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void PlayDisparo() => efectoDisparo.Play();
    public void PlayPowerUp() => efectoPowerUp.Play();
}