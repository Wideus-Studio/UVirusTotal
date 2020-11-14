using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("Menu")]
    [SerializeField] private UIMenu[] _menus = new UIMenu[] { };

    [Header("Settings")]
    [SerializeField] private Animator _settingsAnimator = null;
    [SerializeField] private InputField _settingsApiKey = null;
    [SerializeField] private InputField _settingsMaxCount = null;

    [Header("UITextComponent")]
    [SerializeField] private UITextComponent[] _textComponents = null;

    private void Awake() {
        Game.Saves.SavesManager.LoadData();
        Game.Settings.Load();

        _textComponents = FindObjectsOfType<UITextComponent>();
        ChangeLanguage();
    }

    private void Start() {
        _settingsApiKey.text = VirusTotal.API.APIKEY;
        _settingsMaxCount.text = $"{VirusTotal.API.maxScansList}";
    }

    public UIMenu GetMenuByName(string name) {
        for (int i = 0; i < _menus.Length; i++)
            if (_menus[i].menuName == name) return _menus[i];

        return null;
    }

    public void ChangeMenuByName(string name) {
        for (int i = 0; i < _menus.Length; i++) _menus[i].ChangeVisiable(_menus[i].menuName == name);
    }

    public void ChangeSettingsVisiable() {
        _settingsAnimator.SetTrigger("active");
    }

    public void ApplySettings() {
        VirusTotal.API.APIKEY = _settingsApiKey.text;
        VirusTotal.API.maxScansList = int.Parse(_settingsMaxCount.text);

        Game.Settings.Apply(VirusTotal.API.APIKEY, VirusTotal.API.maxScansList, "ru");
        ChangeLanguage();
    }

    public void ChangeLanguage() {
        for (int i = 0; i < _textComponents.Length; i++) _textComponents[i].UpdateLanguage();
    }
}
