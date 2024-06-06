using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{

    [Header("Settings")]
    [Tooltip("Name of the level scene name")]
    public string levelSceneName;
    //[Tooltip("Continue button gameobject")]
    //public Button continueButton;
    [Tooltip("Slider to controll volume")]
    public Slider volumeSlider;
    [Tooltip("Slider to controll sensitivity")]
    public Slider sensitivitySlider;

    public Button[] interactableButtonsLevel;

    [SerializeField] private GameObject openLevelTwoText;
    [SerializeField] private GameObject openLevelThreeText;

    public void Awake()
    {
        Time.timeScale = 1.0f;

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            sensitivitySlider.value = 50f;
            PlayerPrefs.SetFloat("Sensitivity", 50f);
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            volumeSlider.value = 1f;
            PlayerPrefs.SetFloat("Volume", 1f);
        }
    }

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");

        if (YandexGame.savesData.victoryScore >= 450)
        {
            interactableButtonsLevel[1].interactable = true;
            openLevelThreeText.SetActive(false);
        }
        if (YandexGame.savesData.victoryScore >= 80)
        {
            interactableButtonsLevel[0].interactable = true;
            openLevelTwoText.SetActive(false);
        }

        //Debug.Log("SaveVictoryScore: " + YandexGame.savesData.victoryScore);
        //Debug.Log("SaveLevel: " + YandexGame.savesData.currentLevelSave);
        //Debug.Log("SaveFolder: " + YandexGame.savesData.foldersSave[1]);
        //Debug.Log("SaveItems: " + YandexGame.savesData.collectableItemsSave[1]);
    }

    public void LoadLevel(int scene)
    {
        YandexGame.savesData.currentEnemySave = scene-1;
        YandexGame.savesData.currentLevelSave = 1;
        YandexGame.savesData.currentScene=scene;

        YandexGame.SaveProgress();
        SceneManager.LoadScene("LoadScene");
    }

    public void StartGame(int state)
    {
        if (state == 1)
        {
            ContinueGame();
        }

        if (state == 0)
        {
            StartNewGame();
        }
    }

    public void Configure()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }

    private void ContinueGame()
    {
        //YandexGame.savesData.currentLevelSave = scene;
        //SceneManager.LoadScene(scene);
        SceneManager.LoadScene("LoadScene");
    }

    private void StartNewGame()
    {
        YandexGame.savesData.currentLevelSave = 1;
        SceneManager.LoadScene("LoadScene");
    }
}
