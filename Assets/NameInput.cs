﻿using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour {

    private InputField input;
    public RectTransform namePanel;
    [SerializeField]
    private Text nameLabel;

    public delegate void OnSubmitNameCompleted();
    public OnSubmitNameCompleted onSubmitNameCompleted;

    public void Awake() {
        //ScoreKeeper.Instance.OnReset += restart;
        input = GetComponent<InputField>();
    }

    public void OnDestory() {
        //ScoreKeeper.Instance.OnReset -= restart;
    }

    //private void restart() {
    //    show();
    //}

    public void getName() {
        show();
    }

    private void show() {
        if (PlayerBehaviourData.Instance.playerName == null) {
            input.text = "Nickname";
            input.GetComponentInChildren<Text>().color = Color.gray;
        } else {
            input.text = PlayerBehaviourData.Instance.playerName;
            input.GetComponentInChildren<Text>().color = new Color(.3f, .3f, .3f);
        }

        //GameManager.Instance.pause();
        //namePanel.gameObject.SetActive(true); //someone else should manage this
        input.interactable = true;
        input.ActivateInputField();
    }

    private void hide() {
        //print("name input hide");
        //GameManager.Instance.unpause();
        //namePanel.gameObject.SetActive(false);
    }

    public void onSubmitName() {
        string name = input.text;
        if(string.IsNullOrEmpty(name) || name.Equals("Nickname")) {
            PlayerBehaviourData.Instance.playerName = PlayerBehaviourData.Instance.randomName();
        } else {
            PlayerBehaviourData.Instance.playerName = input.text;
        }
        nameLabel.text = PlayerBehaviourData.Instance.playerName;
        hide(); //TODO: purge
        if(onSubmitNameCompleted != null) { onSubmitNameCompleted(); }
    }
    //TODO: next time there's an ad. if you watch all of it..you'll get...
}
