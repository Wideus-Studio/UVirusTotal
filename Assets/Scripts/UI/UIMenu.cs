using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour {
    public string menuName;

    public virtual void ChangeVisiable(bool value) {
        gameObject.SetActive(value);
    }
}
