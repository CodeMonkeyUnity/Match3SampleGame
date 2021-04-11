using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialize : MonoBehaviour {

    private static bool isInit;

    private void Awake() {
        if (isInit) return; // Only Initialize once
        isInit = true;

        LevelProgression.Init();
        SaveSystem.Init();
    }

}
