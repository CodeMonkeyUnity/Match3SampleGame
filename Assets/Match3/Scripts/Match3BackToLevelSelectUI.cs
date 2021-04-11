using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Match3BackToLevelSelectUI : MonoBehaviour {

    public void BackToLevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

}
