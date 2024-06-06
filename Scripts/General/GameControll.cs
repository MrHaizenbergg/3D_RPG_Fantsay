using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameControll : MonoBehaviour
{
    [Header("General parameters")]
    [Tooltip("count of player life. If life count = 0, game over")]
    public int lifeCount;
    [Tooltip("Player controller script here")]
    public PlayerController player;
    [Tooltip("Player inventory script")]
    public Inventory inventory;
    [Tooltip("Enemy gameobject")]
    public Enemy enemy;
    [Tooltip("Player spawn point")]
    public Transform playerSpawnPoint;
    [Tooltip("Enemy spawn point")]
    public Transform[] enemySpawnPoints;

    public List<Transform> wayPointsA_GameCon;
    public List<Transform> wayPointsB_GameCon;
    public List<Transform> wayPointsC_GameCon;

    private int randomSpawnPoint;

    private bool pause;
    [Tooltip("Hide mouse cursor")]
    public bool hideCursor;

    private bool showTutorial;

    public bool enemyBeginLevel;
    public bool[] unlockedLevel = new bool[4] { true, true, false, false };

    public int currentLevel;

    [Header("UI Settings")]
    public Animation fadeScreen;
    public Animation bloodScreen;
    public Animation lifesCountScreen;
    public string bloodPulsingAnimName;
    public string bloodFadeAnimName;
    public string fadeOutAnimName;
    public string fadeInAnimName;
    public string fadeDieAnimName;
    public string fadeHideDieAnimName;
    public GameObject gameUIControl;
    public GameObject gameControllPanel;
    public GameObject mobileControllPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject endGameNextLevelBtn;
    public GameObject lifesCountBtn;
    public GameObject Tutorial;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public Image dropImage;
    public Image standImage;
    public Image crouchImage;
    public Image hidePlaceExitImage;
    public Image interactImage;

    private SplashWindow splashWindow;
    private ItemSpawner itemSpawner;

    //[SerializeField] private Text textItemsCount;
    [SerializeField] private Text textCollectiblesItemsCount;
    [SerializeField] private Text textEndLevelVictoryPoints;
    [SerializeField] private Text textEndCompanyLoyalty;

    private bool levelCanBeCombpleted = false;

    private int currentVictoryScores;
    private int currentCompanyLoyality;
    [SerializeField] private int MaxLoyality = 5;

    private bool foundItem;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource heroSpeakSource;
    [SerializeField] private AudioClip[] sounds;
    [Tooltip("Screen fade gameobject")]
    public AudioSource dangerAmbient;

    public static GameControll instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogWarning("GameControl more one found");

        randomSpawnPoint = Random.Range(0, enemySpawnPoints.Length);
        splashWindow = GetComponent<SplashWindow>();
        itemSpawner = GetComponent<ItemSpawner>();
        //enemy = enemies[YandexGame.savesData.currentEnemySave];
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += RewardedLifes;
        YandexGame.CloseFullAdEvent += OffCursor;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= RewardedLifes;
        YandexGame.CloseFullAdEvent -= OffCursor;
    }

    private void Start()
    {
        lifeCount += YandexGame.savesData.lifesCount;
        currentLevel = YandexGame.savesData.currentScene;

        currentVictoryScores = 0;
        currentCompanyLoyality = 0;

        foundItem = false;

        if (Application.isMobilePlatform)
        {
            mobileControllPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            mobileControllPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            //StartCoroutine(ShowMessageAboutCrouch());
        }

        if (hideCursor)
        {
            Cursor.visible = false;
        }

        Debug.Log("CursorStart: " + Cursor.visible);

        Time.timeScale = 1.0f;

        //player.locked = true;
        //enemy.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        //fadeScreen.Play("Fade");
        //lifesCountScreen.gameObject.SetActive(true);
        //lifesCountScreen.Play("LifesCount");
        //lifesCountScreen.transform.GetChild(0).GetComponent<Text>().text = lifeCount.ToString();

        //StartCoroutine(WaitRestart());
        //StartCoroutine(ShowMessageAboutMonster());

        if (PlayerPrefs.HasKey("Volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            AudioListener.volume = 1f;
            volumeSlider.value = 1f;
        }

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            player.mouseSensetivity = PlayerPrefs.GetFloat("Sensitivity");
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            player.mouseSensetivity = 50f;
            sensitivitySlider.value = 50f;
        }
    }



    private void Update()
    {
        //AmbientChange();
        ControllGame();
    }

    public void RewardedLifes(int reward)
    {
        Cursor.visible = false;

        if (Application.isMobilePlatform)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        gameOverPanel.SetActive(false);
        lifesCountBtn.SetActive(false);

        StartCoroutine(WaitRestart());
        Debug.Log("RewardLifes");
    }

    private IEnumerator ShowMessageAboutCrouch()
    {
        yield return new WaitForSeconds(6);
        splashWindow.ShowPopUpMessage("Чтобы присесть нажмите C.", "Press C to crouch.");
    }

    public void ShowTutorial()
    {
        Tutorial.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        showTutorial = true;
    }

    public void HideTutorial()
    {
        if (showTutorial)
        {
            Tutorial.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;

            if (Application.isMobilePlatform)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            showTutorial = false;
        }
    }

    public void ShowCursor()
    {
        //Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideCursor()
    {
        //Time.timeScale = 1.0f;
        Cursor.visible = false;

        if (Application.isMobilePlatform)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void ControllGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!pause)
        {
            pause = true;
            pausePanel.SetActive(pause);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            pause = false;
            pausePanel.SetActive(pause);
            Time.timeScale = 1.0f;
            Cursor.visible = false;

            if (Application.isMobilePlatform)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PlayerPrefs.SetInt("HasSaveGame", 0);
        gameOverPanel.SetActive(true);
    }

    public void GameWin()
    {
        if (foundItem)
        {
            levelCanBeCombpleted = true;
            //Debug.Log("LevelCompletedTrue");
        }
        else
        {
            //Debug.Log("У вас нет предмета");
            splashWindow.ShowPopUpMessage("Вы не нашли предмет, найдите его!", "You didn't find the item, find it!");
        }

        if (levelCanBeCombpleted)
        {
            gameUIControl.SetActive(false);

            heroSpeakSource.PlayOneShot(sounds[1]);

            //Debug.Log("Cursor: " + Cursor.visible.ToString());

            if (Language.Instance.currentLanguage == "en")
            {
                textEndLevelVictoryPoints.text = "Difficulty Reward: " + currentVictoryScores;
                textEndCompanyLoyalty.text = "Company loyalty: " + currentCompanyLoyality;
            }
            else if (Language.Instance.currentLanguage == "ru")
            {
                textEndLevelVictoryPoints.text = "Награда за сложность: " + currentVictoryScores;
                textEndCompanyLoyalty.text = "Лояльность Компании: " + currentCompanyLoyality;
            }
            else
            {
                textEndLevelVictoryPoints.text = "Difficulty Reward: " + currentVictoryScores;
                textEndCompanyLoyalty.text = "Company loyalty: " + currentCompanyLoyality;
            }

            YandexGame.savesData.victoryScore += currentVictoryScores;
            YandexGame.savesData.unlockItems[itemSpawner.randomItem] = true;
            //Debug.Log("UnlockItem: " + YandexGame.savesData.unlockItems[itemSpawner.randomItem]);

            if (YandexGame.savesData.companyLoyality < 13)
            {
                YandexGame.savesData.companyLoyality += currentCompanyLoyality;
                //Debug.Log("У вас уже максимальная лояльность");
            }

            YandexGame.NewLeaderboardScores("Scores", YandexGame.savesData.victoryScore);
            YandexGame.SaveProgress();

            //Debug.Log("SaveVictoryScore: " + YandexGame.savesData.victoryScore);
            //Debug.Log("CurrentVictoryScore: " + YandexGame.savesData.victoryScore);
            //Debug.Log("SaveLevel: " + YandexGame.savesData.currentLevelSave);

            gameWinPanel.SetActive(true);
            player.GetComponent<CharacterController>().enabled = false;
            player.enabled = false;
            enemy.gameObject.SetActive(false);
            PlayerPrefs.SetInt("HasSaveGame", 0);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OffCursor()
    {
        Cursor.visible = false;

        if (Application.isMobilePlatform)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenuExit()
    {
        PlayerPrefs.SetInt("HasSaveGame", 1);
        PlayerPrefs.SetInt("LoadGame", SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene(0);
    }

    public void NextLevel(int scoresForContinue)
    {
        if (YandexGame.savesData.victoryScore >= scoresForContinue)
        {
            YandexGame.savesData.currentLevelSave = currentLevel + 1;
            YandexGame.SaveProgress();
            SceneManager.LoadScene("LoadScene");
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void AmbientChange()
    {
        if (enemy.seePlayer)
        {
            if (dangerAmbient.volume < 1)
            {
                dangerAmbient.volume += Time.deltaTime / 2f;
            }

        }
        else
        {
            if (dangerAmbient.volume > 0)
            {
                dangerAmbient.volume -= Time.deltaTime / 8f;
            }
        }

    }

    public void Respawn()
    {
        if (enemyBeginLevel)
            enemy.gameObject.SetActive(true);

        player.clampXaxis.x = 0;
        player.clampXaxis.y = 0;
        player.clampXaxis.x = -90;
        player.clampXaxis.y = 90;
        player.clampByY = false;

        if (player.hidePlace != null)
        {
            player.transform.position = player.hidePlace.outPlace.position;
            // player.transform.rotation = playerSpawnPoint.rotation;
        }

        player.hidePlace = null;
        enemy.transform.position = enemySpawnPoints[randomSpawnPoint].position;
        enemy.transform.rotation = enemySpawnPoints[randomSpawnPoint].rotation;
        player.locked = false;
        player.lockedMovement = false;
        player.cameraTransform.localRotation = new Quaternion(0, 0, 0, 0);
        player.cameraAnimation.Play(player.cameraIdleAnimName);

        if (enemyBeginLevel)
            enemy.RestartEnemyStats();

        ScreenFade(0);

    }

    public void ScreenFade(int state)
    {
        if (state == 0)
        {
            fadeScreen.Play(fadeOutAnimName);
        }

        if (state == 1)
        {
            fadeScreen.Play(fadeInAnimName);
        }

        if (state == 2)
        {

            StartCoroutine(WaitKillAnim(3f));
        }

        if (state == 3)
        {

            StartCoroutine(WaitKillAnim(4f));
        }
    }

    public void ScreenBlood(int state)
    {
        if (state == 0)
        {
            bloodScreen.Play(bloodFadeAnimName);
        }

        if (state == 1)
        {
            bloodScreen.Play(bloodPulsingAnimName);
        }
    }

    private IEnumerator WaitKillAnim(float killTime)
    {
        yield return new WaitForSeconds(killTime);
        fadeScreen.Play(fadeInAnimName);
        StartCoroutine(WaitFadeAnim(fadeInAnimName));
    }

    private IEnumerator WaitFadeAnim(string name)
    {
        yield return new WaitForSeconds(fadeScreen[name].length);
        if (lifeCount > 1)
        {
            lifeCount -= 1;

            if (YandexGame.savesData.lifesCount > 0)
                YandexGame.savesData.lifesCount -= 1;

            YandexGame.SaveProgress();
            lifesCountScreen.gameObject.SetActive(true);
            lifesCountScreen.Play("LifesCount");
            lifesCountScreen.transform.GetChild(0).GetComponent<Text>().text = lifeCount.ToString();

            StartCoroutine(WaitRestart());
        }
        else
        {
            GameOver();
        }

    }

    private IEnumerator WaitRestart()
    {
        yield return new WaitForSeconds(3f);
        lifesCountScreen.gameObject.SetActive(false);
        Respawn();
    }

    public void ConfigureApply()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        player.mouseSensetivity = PlayerPrefs.GetFloat("Sensitivity");

    }
}

