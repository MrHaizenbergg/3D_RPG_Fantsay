using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public PlayerController player;
    public GameControll gameControll;
    public GameObject keypadObject;
    public GameObject hud;
    //public GameObject inv;

    public GameObject animateObject;
    public Animator animator;

    public Text textObject;
    public string answer = "12345";

    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;

    public UnityEvent keypadOpenEvent;

    public bool animate;

    private void Start()
    {
        keypadObject.SetActive(false);
    }

    private void Update()
    {
        if (keypadObject.activeInHierarchy)
        {
            //hud.SetActive(false);
            //inv.SetActive(false);
            //player.lockedMovement = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Number(int number)
    {
        textObject.text += number.ToString();
        button.Play();
    }

    public void Execute()
    {
        if (textObject.text == answer)
        {
            correct.Play();

            if (Language.Instance.currentLanguage == "en")
                textObject.text = "Right";
            else if (Language.Instance.currentLanguage == "ru")
                textObject.text = "Верно";
            else
                textObject.text = "Right";

            animateObject.GetComponent<DoubleSlidingDoorController>().OpenDoor();
            //textObject.text = "Right";
            keypadOpenEvent.Invoke();
        }
        else
        {
            wrong.Play();

            if (Language.Instance.currentLanguage == "en")
                textObject.text = "Wrong";
            else if (Language.Instance.currentLanguage == "ru")
                textObject.text = "Неверно";
            else
                textObject.text = "Wrong";

            //textObject.text = "Wrong";
        }
    }

    public void Clear()
    {
        textObject.text = "";
        button.Play();
    }

    public void Exit()
    {
        keypadObject.SetActive(false);
        //inv.SetActive(false);
        //hud.SetActive(true);
        //player.lockedMovement = false;
        Time.timeScale = 1;
        Cursor.visible = false;

        if (Application.isMobilePlatform)
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("IsMobileExitKeypad");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("IsMobileExitKeypadElse");
        }
    }
}
