using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class level_manager : MonoBehaviour
{
    public static level_manager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _pregressbar; 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   public async void LoadScene(int buildIndex)
    {
        var scene = SceneManager.LoadSceneAsync(buildIndex);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(150);
            _pregressbar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }
}
