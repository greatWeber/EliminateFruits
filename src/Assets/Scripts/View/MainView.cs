using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : EventView {

    public Text Target;
    public Text Score;
    public Text Level;
    public Slider TimeSlider;
    public Sprite[] sprites;
    public Button btn_stop;

    private TouchView touchView;

    private bool isPouse = false;

    public void InitInfo(int target, int score, int level) {
        Target.text = target.ToString();
        Score.text = score.ToString();
        Level.text = level.ToString();
        btn_stop.onClick.AddListener(onStopClick);
        touchView = GameObject.FindObjectOfType<TouchView>();
    }

    public IEnumerator CountDown(float LimitTime) {
        float time = 0f;
        while (time < LimitTime) {
            yield return new WaitForSeconds(1f);
            if (!isPouse) {
                time++;
                TimeSlider.value -= 1 / LimitTime;
            }
           
        }
        //时间到就发送请求
        dispatcher.Dispatch(GameEvents.ViewEvent.Time_UP);

        touchView.isStop = true;
    }

    private new void OnDestroy()
    {
        btn_stop.onClick.RemoveListener(onStopClick);
    }

    void onStopClick() {
        isPouse = !isPouse;
        int index = isPouse ? 1 : 0;
        btn_stop.GetComponent<Image>().sprite = sprites[index];

        touchView.isStop = isPouse;

    }

    public void onChangeScore(int score) {
        Score.text = score.ToString();
    }
}
