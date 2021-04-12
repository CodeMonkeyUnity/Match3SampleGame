using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Match3UI : MonoBehaviour {

    [SerializeField] private Match3 match3;
    [SerializeField] private Color starUnachievedColor;
    [SerializeField] private Color starAchievedColor;

    private TextMeshProUGUI movesText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI targetScoreText;
    private TextMeshProUGUI glassText;
    private Transform winLoseTransform;

    private void Awake() {
        movesText = transform.Find("MovesText").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        glassText = transform.Find("GlassText").GetComponent<TextMeshProUGUI>();
        targetScoreText = transform.Find("TargetScoreText").GetComponent<TextMeshProUGUI>();

        winLoseTransform = transform.Find("WinLose");
        winLoseTransform.gameObject.SetActive(false);

        match3.OnLevelSet += Match3_OnLevelSet;
        match3.OnMoveUsed += Match3_OnMoveUsed;
        match3.OnGlassDestroyed += Match3_OnGlassDestroyed;
        match3.OnScoreChanged += Match3_OnScoreChanged;

        match3.OnOutOfMoves += Match3_OnOutOfMoves;
        match3.OnWin += Match3_OnWin;
    }

    private void Match3_OnWin(object sender, System.EventArgs e) {
        winLoseTransform.gameObject.SetActive(true);
        winLoseTransform.Find("Text").GetComponent<TextMeshProUGUI>().text = "<color=#1ACC23>YOU WIN!</color>";

        LevelProgression.LevelStars levelStars = LevelProgression.LevelStars._1;

        LevelNumberSO levelNumberSO = match3.GetLevelNumberSO();
        LevelSO levelSO = match3.GetLevelSO();

        Debug.Log("GetUsedMoveCount:  " + match3.GetUsedMoveCount());
        switch (levelSO.goalType) {
            case LevelSO.GoalType.Score:
                if (match3.GetUsedMoveCount() <= levelSO.stars3Goal) {
                    levelStars = LevelProgression.LevelStars._3;
                } else {
                    if (match3.GetUsedMoveCount() <= levelSO.stars2Goal) {
                        levelStars = LevelProgression.LevelStars._2;
                    }
                }
                break;
            case LevelSO.GoalType.Glass:
                if (match3.GetUsedMoveCount() <= levelSO.stars3Goal) {
                    levelStars = LevelProgression.LevelStars._3;
                } else {
                    if (match3.GetUsedMoveCount() <= levelSO.stars2Goal) {
                        levelStars = LevelProgression.LevelStars._2;
                    }
                }
                break;
        }

        winLoseTransform.Find("Star_1").GetComponent<Image>().color = starUnachievedColor;
        winLoseTransform.Find("Star_2").GetComponent<Image>().color = starUnachievedColor;
        winLoseTransform.Find("Star_3").GetComponent<Image>().color = starUnachievedColor;

        switch (levelStars) {
            case LevelProgression.LevelStars._0:
                winLoseTransform.Find("Star_1").gameObject.SetActive(false);
                winLoseTransform.Find("Star_2").gameObject.SetActive(false);
                winLoseTransform.Find("Star_3").gameObject.SetActive(false);
                break;

            case LevelProgression.LevelStars._1:
                winLoseTransform.Find("Star_1").GetComponent<Image>().color = starAchievedColor;
                break;
            case LevelProgression.LevelStars._2:
                winLoseTransform.Find("Star_1").GetComponent<Image>().color = starAchievedColor;
                winLoseTransform.Find("Star_2").GetComponent<Image>().color = starAchievedColor;
                break;
            case LevelProgression.LevelStars._3:
                winLoseTransform.Find("Star_1").GetComponent<Image>().color = starAchievedColor;
                winLoseTransform.Find("Star_2").GetComponent<Image>().color = starAchievedColor;
                winLoseTransform.Find("Star_3").GetComponent<Image>().color = starAchievedColor;
                break;
        }

        LevelProgression.SetLevelStars(match3.GetLevelNumberSO(), levelStars);
    }

    private void Match3_OnOutOfMoves(object sender, System.EventArgs e) {
        winLoseTransform.gameObject.SetActive(true);
        winLoseTransform.Find("Text").GetComponent<TextMeshProUGUI>().text = "<color=#CC411A>YOU LOSE!</color>";
    }

    private void Match3_OnScoreChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void Match3_OnGlassDestroyed(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void Match3_OnMoveUsed(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void Match3_OnLevelSet(object sender, System.EventArgs e) {
        LevelSO levelSO = match3.GetLevelSO();

        switch (levelSO.goalType) {
            default:
            case LevelSO.GoalType.Glass:
                transform.Find("GlassImage").gameObject.SetActive(true);
                glassText.gameObject.SetActive(true);

                targetScoreText.gameObject.SetActive(false);
                break;
            case LevelSO.GoalType.Score:
                transform.Find("GlassImage").gameObject.SetActive(false);
                glassText.gameObject.SetActive(false);

                targetScoreText.gameObject.SetActive(true);

                targetScoreText.text = levelSO.targetScore.ToString();
                break;
        }

        UpdateText();
    }

    private void UpdateText() {
        movesText.text = match3.GetMoveCount().ToString();
        scoreText.text = match3.GetScore().ToString();
        glassText.text = match3.GetGlassAmount().ToString();
    }


}
