using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
//    public enum State
//    {
//        Neutral,
//        CapturedHumans,
//        CapturedMonsters
//    }

//    private List<MapAreaCollider> mapAreaCollidersList;
//    private List<HumansMapArea> humansMapAreasInsideList = new List<HumansMapArea>();
//    private List<MonstersMapArea> monstersMapAreasInsideList = new List<MonstersMapArea>();

//    private State state;
//    private float progress;
//    private float progressHumans;
//    private float progressMonsters;

//    private void Awake()
//    {
//        mapAreaCollidersList = new List<MapAreaCollider>();

//        foreach (Transform child in transform)
//        {
//            MapAreaCollider mapAreaCollider = child.GetComponent<MapAreaCollider>();
//            if (mapAreaCollider != null)
//            {
//                mapAreaCollidersList.Add(mapAreaCollider);
//            }
//        }

//        state = State.Neutral;
//    }

//    private void Update()
//    {
//        //if (mapAreaCollidersList)
//        //{
//            switch (state)
//            {
//                case State.Neutral:
//                    CalculateCaptured();
//                    break;
//                case State.CapturedHumans:
//                    CalculateCaptured();
//                    break;
//                case State.CapturedMonsters:
//                    CalculateCaptured();
//                    break;
//            }
//        //}
//    }

//    private void CalculateCaptured()
//    {
//        //List<HumansMapArea> humansMapAreasInsideList = new List<HumansMapArea>();
//        //List<MonstersMapArea> monstersMapAreasInsideList = new List<MonstersMapArea>();

//        foreach (MapAreaCollider mapAreaCollider in mapAreaCollidersList)
//        {
//            foreach (HumansMapArea humansMapArea in mapAreaCollider.GetHumansMapAreasList())
//            {
//                if (!humansMapAreasInsideList.Contains(humansMapArea))
//                {
//                    humansMapAreasInsideList.Add(humansMapArea);
//                }             
//            }
//            foreach (MonstersMapArea monstersMapArea in mapAreaCollider.GetMonstersMapAreasList())
//            {
//                if (!monstersMapAreasInsideList.Contains(monstersMapArea))
//                {
//                    monstersMapAreasInsideList.Add(monstersMapArea);
//                }
//            }
//        }

//        float progressSpeed = 0.5f;

//        progressHumans = humansMapAreasInsideList.Count * progressSpeed * Time.deltaTime;
//        progressMonsters = monstersMapAreasInsideList.Count * progressSpeed * Time.deltaTime;

//        if (humansMapAreasInsideList != null)
//        {
//            if (progressHumans >= progressMonsters)
//                progress += progressHumans;
//        }
//        if (monstersMapAreasInsideList != null)
//        {
//            if (progressHumans <= progressMonsters)
//                progress -= progressMonsters;
//        }

//        Debug.Log("humansCountInsideMapArea: " + humansMapAreasInsideList.Count + "; progress" + progress);
//        Debug.Log("monstersCountInsideMapArea: " + monstersMapAreasInsideList.Count + "; progress" + progress);


//        if (progress >= 1f)
//        {
//            state = State.CapturedHumans;
//            Debug.Log("CapturedHumans");
//        }
//        if (progress <= -1f)
//        {
//            state = State.CapturedMonsters;
//            Debug.Log("CapturedMonsters");
//        }
//        progress = Mathf.Clamp(progress, -1.1f, 1.1f);
//    }
}
