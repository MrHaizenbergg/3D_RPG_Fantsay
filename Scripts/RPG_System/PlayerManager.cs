using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player;
    private PlayerStats playerStats;

    public QuestRPG[] quests;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogWarning("PlayerManager more one found");

        playerStats=GetComponent<PlayerStats>();
    }


    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
