using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAreasCapturingUI : MonoBehaviour
{
    [SerializeField] private List<MapAreaCollider> mapList;

    private MapAreaCollider mapAreaCollider;
    private Image progressImage;
    private Image progressImageMonsters;

    private void Awake()
    {
        progressImage = transform.Find("ProgressImage").GetComponent<Image>();
        progressImageMonsters = transform.Find("ProgressImageMonsters").GetComponent<Image>();
    }

    private void Start()
    {
        foreach (MapAreaCollider mapArea in mapList)
        {
            mapArea.OnPlayerEnter += MapArea_OnPlayerEnter;
            mapArea.OnPlayerExit += MapArea_OnPlayerExit;
        }

        Hide();
    }

    private void Update()
    {
        if (mapAreaCollider.GetProgress() > 0)
        {
            progressImageMonsters.enabled = false;
            progressImage.enabled = true;
            progressImage.fillAmount = mapAreaCollider.GetProgress();
        }
        else if(mapAreaCollider.GetProgress() < 0)
        {
            progressImageMonsters.enabled = true;
            progressImage.enabled = false;
            progressImageMonsters.fillAmount = -mapAreaCollider.GetProgress();
        }
    }

    private void MapArea_OnPlayerExit(object sender, EventArgs e)
    {
        Hide();
    }

    private void MapArea_OnPlayerEnter(object sender, EventArgs e)
    {
        mapAreaCollider = sender as MapAreaCollider;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
