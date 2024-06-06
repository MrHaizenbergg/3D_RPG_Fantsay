using UnityEngine;
using YG;

public class ClearDataSave : MonoBehaviour
{
    public void ClearSave()
    {
        YandexGame.savesData.currentScene = 1;
        YandexGame.savesData.lifesCount = 2;
        YandexGame.savesData.currentEnemySave = 0;
        YandexGame.savesData.victoryScore = 0;
        YandexGame.savesData.companyLoyality = 0;
        YandexGame.savesData.availablePoints = 0;
        YandexGame.savesData.spentPoints = 0;

        YandexGame.savesData.unlockItems = new bool[8] { false, false, false, false, false, false, false, false };

        YandexGame.savesData.hideScoresFromLoyality = 0;
        YandexGame.savesData.speedHeroFromLoyality = 0;
        YandexGame.savesData.speedEnemyFromLoyality = 0;
        YandexGame.SaveProgress();
    }
}
