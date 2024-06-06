using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class SceneLoader : MonoBehaviour {

    
    public bool isStartScene;
    [HideInInspector]
    //public string sceneName;
    public int sceneIndex;
    [Tooltip("Name of Main Menu scene")]
    //public string mainMenuSceneName;
    public int mainMenuSceneIndex;
    public Image loadingBar;

    private void Awake()
    {
        if (!isStartScene)
        {
            //sceneName = PlayerPrefs.GetString("SceneName");
            //sceneIndex = PlayerPrefs.GetInt("LoadGame");
            sceneIndex = YandexGame.savesData.currentLevelSave;
        }else
        {
            //sceneName = mainMenuSceneName;
            sceneIndex = mainMenuSceneIndex;
        }
    }

    private void Start()
    {
        StartCoroutine(SceneLoad());
    }

    IEnumerator SceneLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingBar.fillAmount = progress;
            yield return null;
        }

    }

}
