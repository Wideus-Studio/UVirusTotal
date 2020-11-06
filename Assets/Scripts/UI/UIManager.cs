using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private Animator _settingsAnimator = null;
    [SerializeField] private InputField _settingsApiKey = null;
    [SerializeField] private InputField _settingsMaxCount = null;

    private void Start() {
        _settingsApiKey.text = VirusTotal.API.APIKEY;
        _settingsMaxCount.text = $"{VirusTotal.API.maxScansList}";
    }

    public void ChangeSettingsVisiable() {
        _settingsAnimator.SetTrigger("active");
    }

    public void ApplySettings() {
        VirusTotal.API.APIKEY = _settingsApiKey.text;
        VirusTotal.API.maxScansList = int.Parse(_settingsMaxCount.text);
    }
}
