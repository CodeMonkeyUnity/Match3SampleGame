using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public static class LevelProgression {

    public enum LevelStars {
        _0,
        _1,
        _2,
        _3
    }

    public static Dictionary<LevelNumberSO, LevelStars> levelNumberStarsDic;



    public static void Init() {
        levelNumberStarsDic = new Dictionary<LevelNumberSO, LevelStars>();

        foreach (LevelNumberSO levelNumberSO in Match3Assets.Instance.levelNumberSOList) {
            levelNumberStarsDic[levelNumberSO] = LevelStars._0;
        }
    }

    public static bool IsLevelUnlocked(LevelNumberSO levelNumberSO) {
        int levelIndex = GetLevelNumberIndex(levelNumberSO);

        if (levelIndex == 0) {
            // First level
            return true;
        } else {
            // Not first level, is previous one done?
            LevelNumberSO previousLevelNumberSO = Match3Assets.Instance.levelNumberSOList[levelIndex - 1];
            if (GetLevelStars(previousLevelNumberSO) != LevelStars._0) {
                return true;
            }
        }

        return false;
    }

    private static LevelNumberSO GetLevelNumberSO(int levelNumber) {
        for (int i = 0; i < Match3Assets.Instance.levelNumberSOList.Count; i++) {
            if (Match3Assets.Instance.levelNumberSOList[i].levelNumber == levelNumber) {
                return Match3Assets.Instance.levelNumberSOList[i];
            }
        }

        Debug.LogError("Cannot find Level with Number " + levelNumber);
        return null;
    }

    private static int GetLevelNumberIndex(LevelNumberSO levelNumberSO) {
        for (int i=0; i<Match3Assets.Instance.levelNumberSOList.Count; i++) {
            if (Match3Assets.Instance.levelNumberSOList[i] == levelNumberSO) {
                return i;
            }
        }

        Debug.LogError("Cannot find Level Number Index!");
        return -1;
    }

    public static void SetLevelStars(LevelNumberSO levelNumberSO, LevelStars levelStars) {
        levelNumberStarsDic[levelNumberSO] = levelStars;
        SaveSystem.Save();
    }

    public static LevelStars GetLevelStars(LevelNumberSO levelNumberSO) {
        return levelNumberStarsDic[levelNumberSO];
    }

    public static string Save() {
        List<SaveData.LevelSaveSingle> levelSaveSingleList = new List<SaveData.LevelSaveSingle>();

        foreach (LevelNumberSO levelNumberSO in levelNumberStarsDic.Keys) {
            levelSaveSingleList.Add(new SaveData.LevelSaveSingle {
                levelNumber = levelNumberSO.levelNumber,
                levelStars = levelNumberStarsDic[levelNumberSO],
            });
        }

        SaveData saveData = new SaveData {
            levelSaveSingleList = levelSaveSingleList,
        };

        return JsonUtility.ToJson(saveData);
    }

    public static void Load(string saveString) {
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);

        foreach (SaveData.LevelSaveSingle levelSaveSingle in saveData.levelSaveSingleList) {
            LevelNumberSO levelNumberSO = GetLevelNumberSO(levelSaveSingle.levelNumber);
            levelNumberStarsDic[levelNumberSO] = levelSaveSingle.levelStars;
        }
    }

    [System.Serializable]
    private class SaveData {

        [System.Serializable]
        public class LevelSaveSingle {
            public int levelNumber;
            public LevelStars levelStars;
        }

        public List<LevelSaveSingle> levelSaveSingleList;
    }

}
