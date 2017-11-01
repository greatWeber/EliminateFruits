using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// 控制每个关卡
/// </summary>
public class MapView : MonoBehaviour, IPointerClickHandler {

    public int Level;

    private bool locked = true;

    public Sprite[] states;

    public Button button;

    public bool Locked {
        get { return locked; }
        set {
            if (!value)
                GetComponentInChildren<Text>().text = Level.ToString();
            int index = value ? 0 : 1;
            GetComponent<Image>().sprite = states[index];
            locked = value;
        }
    }

    void Start() {
        button = GetComponent<Button>();
    }

    public void GetStar(int score, int target) {
        if (score >= target && score < target * 1.5) {
            GetComponent<Image>().sprite = states[2];
        } else if (score >= target * 1.5 && score < target * 2) {
            GetComponent<Image>().sprite = states[3];
        } else if (score >= target*2) {
            GetComponent<Image>().sprite = states[4];
        }
    }

    public Action<int> onClick;

    //点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (!Locked && onClick != null) {
            onClick(this.Level);
        }
    }

    private void OnDestroy()
    {
        onClick = null;
    }
}
