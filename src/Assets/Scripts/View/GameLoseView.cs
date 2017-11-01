using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoseView : EventView {

    public Text Target;
    public Text Score;

    public Button btn_menu;
    public Button btn_restart;

    public Animator LoseShow;

    public void Show(int target, int score) {
        gameObject.SetActive(true);
        LoseShow.SetTrigger("show");
        Target.text = target.ToString();
        Score.text = score.ToString();
    }

    private new void OnDestroy()
    {
        btn_menu.onClick = null;
        btn_restart.onClick = null;
    }
}
