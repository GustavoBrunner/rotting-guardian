using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public string sceneName; // Nome da cena a ser carregada
    public float delay = 2.0f; // Tempo de espera antes de carregar a cena

    private bool isClicked = false;

    [SerializeField] private SceneTransition transition;
    [SerializeField] private FMODUnity.StudioEventEmitter doorSFX;
    // Textura do cursor customizado
    public Texture2D customCursor;

    // Posição do hotspot no cursor (normalmente o ponto onde "clicamos")
    public Vector2 cursorHotspot = Vector2.zero;

    // Textura padrão do cursor
    private Texture2D defaultCursor;

    private void Start()
    {
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            doorSFX.Play();
            Invoke("LoadScene", delay); // Chama o método após o tempo especificado
        }
    }

    private void LoadScene()
    {
        transition.StartSceneTransition();
        //SceneManager.LoadScene(sceneName); // Carrega a cena
    }

    void OnMouseEnter()
    {
        // Quando o mouse entra no collider, muda o cursor
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        // Quando o mouse sai do collider, volta ao cursor padrão
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
