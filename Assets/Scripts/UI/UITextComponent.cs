using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextComponent : MonoBehaviour {
    [SerializeField] private string _key = "None";
    [SerializeField] private Text _value = null;

    public void UpdateLanguage() {
        _value.text = Game.Assets.Data.GetStringByLanguage(_key);
    }
}
