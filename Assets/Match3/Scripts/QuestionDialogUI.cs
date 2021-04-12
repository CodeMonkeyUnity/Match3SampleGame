using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionDialogUI : MonoBehaviour {
    
    public static QuestionDialogUI Instance { get; private set; }


    private TextMeshProUGUI questionText;
    private Action yesAction;
    private Action noAction;

    private void Awake() {
        Instance = this;

        questionText = transform.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        transform.Find("YesBtn").GetComponent<Button>().onClick.AddListener(YesBtn);
        transform.Find("NoBtn").GetComponent<Button>().onClick.AddListener(NoBtn);

        Hide();
    }


    private void YesBtn() {
        Hide();
        yesAction();
    }

    private void NoBtn() {
        Hide();
        noAction();
    }

    public void Show(string questionString, Action yesAction, Action noAction) {
        questionText.text = questionString;
        this.yesAction = yesAction;
        this.noAction = noAction;

        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

}
