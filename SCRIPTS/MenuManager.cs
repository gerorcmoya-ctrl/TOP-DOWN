using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelOpciones;

    public void Jugar()
        => SceneManager.LoadScene(1);

    public void AbrirOpciones()
    {
        panelMenu.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void VolverAlPanelMenu()
    {
        panelOpciones.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}