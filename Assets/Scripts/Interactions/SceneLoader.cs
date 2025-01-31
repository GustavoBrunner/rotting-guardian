using UnityEngine;
using UnityEngine.SceneManagement; // Importa o namespace necessário para gerenciamento de cenas

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    [SerializeField] public static SceneLoader Instance { get => _instance; }


    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }
    // Função para carregar uma cena pelo nome
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Função para carregar uma cena pelo índice (opcional)
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
