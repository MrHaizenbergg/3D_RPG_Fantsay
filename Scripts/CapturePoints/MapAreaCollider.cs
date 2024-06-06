using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaCollider : MonoBehaviour
{
    public event EventHandler OnCaptured;
    public event EventHandler OnCapturedMonsters;

    public event EventHandler OnPlayerEnter;
    public event EventHandler OnPlayerExit;

    private List<HumansMapArea> humansMapAreasList = new List<HumansMapArea>();
    private List<MonstersMapArea> monstersMapAreasList = new List<MonstersMapArea>();

    public float progressSpeed = 0.1f;

    public enum State
    {
        Neutral,
        CapturedHumans,
        CapturedMonsters
    }

    public int numberOfPoint;

    public State state;
    private float progress;
    private float progressHumans;
    private float progressMonsters;

    //bool hasHumanInside=false;

    private void Awake()
    {
        state = State.Neutral;
    }

    private void Update()
    {
        if (humansMapAreasList.Count > 0 || monstersMapAreasList.Count > 0)
        {
            switch (state)
            {
                case State.Neutral:
                    CalculateCaptured();
                    break;
                case State.CapturedHumans:
                    CalculateCaptured();
                    break;
                case State.CapturedMonsters:
                    CalculateCaptured();
                    break;
            }
        }
    }

    private void MapAreaCollider_HumanExit(object sender, EventArgs e)
    {
        bool hasHumanInside = false;
        if (humansMapAreasList.Count > 0)
        {
            hasHumanInside = true;
        }

        if(!hasHumanInside)
        {
            OnPlayerExit?.Invoke(this,EventArgs.Empty);
        }
    }

    private void CheckPersons()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<HumansMapArea>(out HumansMapArea humansMapAreas))
        {
            humansMapAreasList.Add(humansMapAreas);
            //OnPlayerEnter?.Invoke(this, EventArgs.Empty);
            Debug.Log("HumanEntered");
        }
        if(collider.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnPlayerEnter?.Invoke(this, EventArgs.Empty);
        }
        if (collider.TryGetComponent<MonstersMapArea>(out MonstersMapArea monstersMapAreas))
        {
            monstersMapAreasList.Add(monstersMapAreas);
            Debug.Log("MonsterEntered");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<HumansMapArea>(out HumansMapArea humansMapAreas))
        {
            humansMapAreasList.Remove(humansMapAreas);
            //OnPlayerExit?.Invoke(this, EventArgs.Empty);
            Debug.Log("HumanOut");
        }
        if (collider.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnPlayerExit?.Invoke(this, EventArgs.Empty);
        }
        if (collider.TryGetComponent<MonstersMapArea>(out MonstersMapArea monstersMapAreas))
        {
            monstersMapAreasList.Remove(monstersMapAreas);
            Debug.Log("MonsterOut");
        }
    }

    public List<HumansMapArea> GetHumansMapAreasList()
    {
        return humansMapAreasList;
    }
    public List<MonstersMapArea> GetMonstersMapAreasList()
    {
        return monstersMapAreasList;
    }

    private void CalculateCaptured()
    {
        progressHumans = humansMapAreasList.Count * progressSpeed * Time.deltaTime;
        progressMonsters = monstersMapAreasList.Count * progressSpeed * Time.deltaTime;

        if (humansMapAreasList != null || monstersMapAreasList != null)
        {
            if (progressHumans >= progressMonsters)
            {
                float progressHum = progressHumans - progressMonsters;
                progress += progressHum;
            }
            if (progressHumans <= progressMonsters)
            {
                float progressMon = progressMonsters - progressHumans;
                progress -= progressMon;
            }
        }
        //Debug.Log("humansCountInsideMapArea: " + humansMapAreasList.Count + "; progress" + progress);
        //Debug.Log("monstersCountInsideMapArea: " + monstersMapAreasList.Count + "; progress" + progress);
        if (progress >= 1f && state!=State.CapturedHumans)
        {
            state = State.CapturedHumans;
            OnCaptured?.Invoke(this, EventArgs.Empty);
            Debug.Log("CapturedHumans");
        }
        if (progress <= -1f && state!=State.CapturedMonsters)
        {
            state = State.CapturedMonsters;
            OnCapturedMonsters?.Invoke(this, EventArgs.Empty);
            Debug.Log("CapturedMonsters");
        }
        progress = Mathf.Clamp(progress, -1.0f, 1.0f);
    }

    public float GetProgress()
    {
        return progress;
    }
}
