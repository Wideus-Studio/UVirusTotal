using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour {
    public virtual void ChangeVisiable() {
        gameObject.SetActive(!gameObject.active);
    }
}
