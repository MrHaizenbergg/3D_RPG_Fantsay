using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAreaUI : MonoBehaviour
{
    public Text scoreMonstersText;
    public Text scoreHumansText;
    public Image monsterProgressImg;
    public Image humansProgressImg;

    public float progressSpeed = 0.3f;
    private int scoreMonsters = 0;
    private int scoreHumans = 0;

    private int pointsForHumans = 0;
    private int pointsForMonsters = 0;
    public int maxPoints = 3;
    public int scoreWins = 300;

    private float tickCooldown = 3;


    [System.Serializable]
    public class MapAreaImage
    {
        public Image uiImage;
        public MapAreaCollider mapAreaCollider;
    }

    [SerializeField] private List<MapAreaImage> mapAreaImageList;

    private void Awake()
    {
        scoreMonsters = 0;
        scoreHumans = 0;
        pointsForHumans = 0;
        pointsForMonsters = 0;
    }

    private void Start()
    {
        foreach (MapAreaImage mapAreaImage in mapAreaImageList)
        {
            mapAreaImage.mapAreaCollider.OnCaptured += (object sender, EventArgs e) =>
            { mapAreaImage.uiImage.color = Color.green; };

            mapAreaImage.mapAreaCollider.OnCapturedMonsters += (object sender, EventArgs e) =>
            { mapAreaImage.uiImage.color = Color.red; };

            mapAreaImage.mapAreaCollider.OnCaptured += CapturedPointCalculate;

            mapAreaImage.mapAreaCollider.OnCapturedMonsters += CapturedPointCalculate;
        }
    }

    private void CapturedPointCalculate(object sender, EventArgs e)
    {
        pointsForHumans = 0;
        pointsForMonsters = 0;

        foreach (MapAreaImage mapAreaImage in mapAreaImageList)
        {
            if (mapAreaImage.mapAreaCollider.state == MapAreaCollider.State.CapturedHumans)
            {
                pointsForHumans++;
                Debug.Log("PointForHumans: " + pointsForHumans);
            }
            if (mapAreaImage.mapAreaCollider.state == MapAreaCollider.State.CapturedMonsters)
            {
                pointsForMonsters++;
                Debug.Log("PointForMonsters: " + pointsForMonsters);
            }
        }
    }

    private void TickScore()
    {
        if (scoreMonsters >= scoreWins || scoreHumans >= scoreWins)
        {
            //VictoryGame
            if (scoreMonsters >= scoreWins)
            {
                Debug.Log("Monsters Victory");
                return;
            }
            if (scoreHumans >= scoreWins)
            {
                Debug.Log("Humans Victory");
                return;
            }
        }

        if (tickCooldown <= 0)
        {
            scoreHumans += pointsForHumans;
            scoreMonsters += pointsForMonsters;
            Debug.Log("ScoreHumans: " + scoreHumans);

            humansProgressImg.fillAmount = (float)scoreHumans / (float)scoreWins;
            monsterProgressImg.fillAmount = (float)scoreMonsters / (float)scoreWins;

            scoreHumansText.text = scoreHumans.ToString();
            scoreMonstersText.text = scoreMonsters.ToString();

            tickCooldown = 3;
        }
    }

    private void Update()
    {
        tickCooldown -= Time.deltaTime;

        TickScore();
    }
}
