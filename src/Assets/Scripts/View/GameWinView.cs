using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinView : View {

    public Text Target;
    public Text Score;

    public Button btn_menu;
    public Button btn_retry;
    public Button btn_next;

    public Animator WinShow;

    public void Show(int target, int score)
    {
        gameObject.SetActive(true);
        WinShow.SetTrigger("show");
        Target.text = target.ToString();
        Score.text = score.ToString();
    }

    private new void OnDestroy()
    {
        btn_menu.onClick = null;
        btn_retry.onClick = null;
        btn_next.onClick = null;
    }

}
