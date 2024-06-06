using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashWindow : MonoBehaviour
{
    [SerializeField] private Text _textSplash;

    [SerializeField] private GameObject _window;
    [SerializeField] private Animator _splashAnimator;

    [SerializeField] private Queue<string> splashQueue;
    private Coroutine _queueChecker;

    private void Start()
    {
        _window.SetActive(false);
        splashQueue = new Queue<string>();
    }

    private void AddToQueue(string text)
    {
        if (splashQueue.Count < 2)
        {
            splashQueue.Enqueue(text);
            if (_queueChecker == null)
                _queueChecker = StartCoroutine(CheckQueue());
        }
    }

    private void ShowPopup(string text)
    {
        _window.SetActive(true);
        _textSplash.text = text;
        _splashAnimator.Play("MessageOn");
    }

    private IEnumerator CheckQueue()
    {
        do
        {
            ShowPopup(splashQueue.Dequeue());
            do
            {
                yield return null;
            }
            while (!_splashAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));
        }
        while (splashQueue.Count > 0);
        {
            _window.SetActive(false);
            _queueChecker = null;
        }
    }

    public void ShowPopUpMessage(string ruMessage, string enMessage)
    {
        if (Language.Instance.currentLanguage == "en")
            AddToQueue(enMessage);
        else if (Language.Instance.currentLanguage == "ru")
            AddToQueue(ruMessage);
        else
            AddToQueue(enMessage);
    }
}
