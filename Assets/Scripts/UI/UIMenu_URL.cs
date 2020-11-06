using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu_URL : UIMenu {
    [SerializeField] private InputField _url = null;
    [SerializeField] private Button _checkButton = null;
    [SerializeField] private Button _reportButton = null;
    [SerializeField] private ReportElement _reportElement = null;
    [SerializeField] private Thread _threadCheck = null;

    private void Update() {
        if (_threadCheck != null) {
            if (!_threadCheck.IsAlive) {
                ReportByUrl();
                _threadCheck = null;
            }
        }
    }

    public void CheckByUrl() {
        string URL = _url.text;
        _threadCheck = new Thread(() => { VirusTotal.API.ScanURL(URL); VirusTotal.API.ReportURL(); });
        _threadCheck.Start();
    }

    public void ReportByUrl() {
        if (VirusTotal.API.scans.Count > 0) _reportElement.Init(VirusTotal.API.scans[0]);
    }
}
