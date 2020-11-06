using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportElement : MonoBehaviour {
    [SerializeField] private Text _scanID = null;
    [SerializeField] private Text _date = null;
    [SerializeField] private Text _positive = null;
    [SerializeField] private Text _total = null;

    public void Init(VirusTotal.ScanResult scan) {
        _scanID.text = scan.scan_id;
        _date.text = scan.date;
        _positive.text = $"{scan.positives}";
        _total.text = $"{scan.total}";
    }
}
