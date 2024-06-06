using UnityEngine;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] sentences;

    [TextArea(3, 10)]
    public string[] englishSentences;

    [SerializeField] private Text textSplash;
    [SerializeField] private GameObject noteObject;

    private void Start()
    {
        noteObject.SetActive(false);
    }

    public void CloseNote()
    {
        Time.timeScale = 1;
        Cursor.visible = false;

        if (Application.isMobilePlatform)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        noteObject.SetActive(false);
    }

    public void ShowNote(int note)
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AddTextNote(note);
        noteObject.SetActive(true);
    }

    private void AddTextNote(int message)
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            if (i == message)
            {
                if (Language.Instance.currentLanguage == "en")
                    textSplash.text = englishSentences[i];
                else if (Language.Instance.currentLanguage == "ru")
                    textSplash.text = sentences[i];
                else
                    textSplash.text = englishSentences[i];

                //textSplash.text = sentences[i];
            }
        }
    }


}
