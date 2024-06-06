using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YandexAdvController : MonoBehaviour
{
    public static YandexAdvController Instance;
    public static bool gameIsPaused;

    //[DllImport("__Internal")]
    //private static extern void ShowAdv();

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

    //private void OnEnable()
    //{
    //    YandexGame.OpenFullAdEvent += SoundOffAdv;
    //    YandexGame.CloseFullAdEvent += CloseAdv;
    //    Debug.Log("Podpiska");
    //}

    //private void OnDisable()
    //{
    //    YandexGame.OpenFullAdEvent -= SoundOffAdv;
    //    YandexGame.CloseFullAdEvent -= CloseAdv;
    //    Debug.Log("Otpiska");
    //}

    //public void ShowAdvController()
    //{
        //ShowAdv();
    //}

    //void PauseGame()
    //{
    //    if (gameIsPaused)
    //    {
    //        Time.timeScale = 0f;
    //    }
    //    else
    //    {
    //        Time.timeScale = 1;
    //    }
    //}


    //#if UNITY_WEBGL
    public void CloseAdv()
    {
        Time.timeScale = 1;
        //gameIsPaused = false;
        AudioListener.pause = false;
        Debug.Log("CloseAdvTimeScale: "+ Time.timeScale);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);       
    }

    public void SoundOffAdv()
    {
        Time.timeScale = 0;
        //gameIsPaused = true;
        AudioListener.pause = true;
        Debug.Log("SoundOffAdvTimeScale: " + Time.timeScale);
    }
//#endif
}
