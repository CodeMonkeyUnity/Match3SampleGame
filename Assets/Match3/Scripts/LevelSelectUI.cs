using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectUI : MonoBehaviour {

    private static bool unlockAllLevels;

    [SerializeField] private Color levelLockedColor;
    [SerializeField] private Color starUnachievedColor;
    [SerializeField] private Color starAchievedColor;


    private void Awake() {
        Transform levelContainer = transform.Find("LevelContainer");

        foreach (Transform levelTransform in levelContainer) {
            LevelNumberSO levelNumberSO = levelTransform.GetComponent<LevelNumberSOHolder>().levelNumberSO;

            if (unlockAllLevels || LevelProgression.IsLevelUnlocked(levelNumberSO)) {
                // Level Unlocked
                levelTransform.GetComponent<Button>().enabled = true;

                levelTransform.Find("Star_1").gameObject.SetActive(true);
                levelTransform.Find("Star_2").gameObject.SetActive(true);
                levelTransform.Find("Star_3").gameObject.SetActive(true);

                levelTransform.Find("Star_1").GetComponent<Image>().color = starUnachievedColor;
                levelTransform.Find("Star_2").GetComponent<Image>().color = starUnachievedColor;
                levelTransform.Find("Star_3").GetComponent<Image>().color = starUnachievedColor;

                LevelProgression.LevelStars levelStars = LevelProgression.GetLevelStars(levelNumberSO);
                switch (levelStars) {
                    case LevelProgression.LevelStars._0:
                        levelTransform.Find("Star_1").gameObject.SetActive(false);
                        levelTransform.Find("Star_2").gameObject.SetActive(false);
                        levelTransform.Find("Star_3").gameObject.SetActive(false);
                        break;

                    case LevelProgression.LevelStars._1:
                        levelTransform.Find("Star_1").GetComponent<Image>().color = starAchievedColor;
                        break;
                    case LevelProgression.LevelStars._2:
                        levelTransform.Find("Star_1").GetComponent<Image>().color = starAchievedColor;
                        levelTransform.Find("Star_2").GetComponent<Image>().color = starAchievedColor;
                        break;
                    case LevelProgression.LevelStars._3:
                        levelTransform.Find("Star_1").GetComponent<Image>().color = starAchievedColor;
                        levelTransform.Find("Star_2").GetComponent<Image>().color = starAchievedColor;
                        levelTransform.Find("Star_3").GetComponent<Image>().color = starAchievedColor;
                        break;
                }
            } else {
                // Level Locked
                levelTransform.GetComponent<Button>().enabled = false;
                levelTransform.GetComponent<Image>().color = levelLockedColor;

                levelTransform.Find("Star_1").gameObject.SetActive(false);
                levelTransform.Find("Star_2").gameObject.SetActive(false);
                levelTransform.Find("Star_3").gameObject.SetActive(false);
            }
        }

        transform.Find("LockUnlockAllBtn").Find("Text").GetComponent<TextMeshProUGUI>().text = (unlockAllLevels ? "LOCK" : "UNLOCK") + " ALL LEVELS";
    }

    public void LoadLevel(LevelNumberSO levelNumberSO) {
        Match3.LoadLevel(levelNumberSO);
    }

    public void UnlockAllLevels() {
        unlockAllLevels = !unlockAllLevels;
        SceneManager.LoadScene("LevelSelect");
    }

}
