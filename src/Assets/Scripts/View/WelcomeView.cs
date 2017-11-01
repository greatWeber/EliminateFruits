using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeView : View {

    public Button btn_play;
    public Button btn_music;
    public Image img_music;

    public Sprite[] musicState;

    public void changeMusicState(bool mute) {
        int index = mute ? 1 : 0;
        img_music.sprite = musicState[index];
    }

}
