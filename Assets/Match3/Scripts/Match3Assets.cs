using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Assets : MonoBehaviour {

    // Internal instance reference
    private static Match3Assets instance;

    // Instance reference
    public static Match3Assets Instance {
        get {
            if (instance == null) instance = Instantiate(Resources.Load<Match3Assets>(nameof(Match3Assets)));
            return instance;
        }
    }


    public List<LevelNumberSO> levelNumberSOList;

    public LevelSO level_A;
    public LevelSO level_B;
    public LevelSO level_C;
    public LevelSO level_D;
    public LevelSO level_E;

}
