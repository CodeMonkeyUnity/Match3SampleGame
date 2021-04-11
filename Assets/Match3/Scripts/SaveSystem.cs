using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem {

    public static void Init() {
        Load();
    }

    public static void Save() {
        SaveData saveData = new SaveData {
            levelProgressionSave = LevelProgression.Save()
        };
        PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(saveData));
        PlayerPrefs.Save();
    }

    public static void Load() {
        string loadSaveData = PlayerPrefs.GetString("SaveData", "");

        if (loadSaveData != "") {
            SaveData saveData = JsonUtility.FromJson<SaveData>(loadSaveData);

            LevelProgression.Load(saveData.levelProgressionSave);
        }
    }

    [System.Serializable]
    private class SaveData {
        public string levelProgressionSave;
    }

}
