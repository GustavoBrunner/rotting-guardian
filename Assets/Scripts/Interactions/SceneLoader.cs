using UnityEngine;
using UnityEngine.SceneManagement; // Importa o namespace necess�rio para gerenciamento de cenas

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    [SerializeField] public static SceneLoader Instance { get => _instance; }


    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }
    // Fun��o para carregar uma cena pelo nome
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Fun��o para carregar uma cena pelo �ndice (opcional)
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
