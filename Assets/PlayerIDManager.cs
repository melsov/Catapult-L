using UnityEngine;
using System.Collections;
using System;

public class PlayerIDManager : Singleton<PlayerIDManager> {
    protected PlayerIDManager() { }

    private Action _doneSettingUpPlayerName;

    [SerializeField]
    private RectTransform namePanel;

    private NameInput _nameInput;
    private NameInput nameInput {
        get {
            if(!_nameInput) {
                //missing ref exception?
                if (!namePanel) {
                    print("name panel null");
                } else print("name panel not null");

                _nameInput = namePanel.GetComponentInChildren<NameInput>();
            }
            return _nameInput;
        }
    }

    public void OnEnable() {
        nameInput.onSubmitNameCompleted += playerNameSetupCompleted;
    }

    public void OnDisable() {
        if (_nameInput) {
            _nameInput.onSubmitNameCompleted -= playerNameSetupCompleted;
        }
    }

    private void playerNameSetupCompleted() {
        _doneSettingUpPlayerName.Invoke();
        namePanel.gameObject.SetActive(false);
    }

    public void setupPlayerName(Action doneSettingUpPlayerName) {
        namePanel.gameObject.SetActive(true);
        _doneSettingUpPlayerName = doneSettingUpPlayerName;
        nameInput.getName();
    }
}
