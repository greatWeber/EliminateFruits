using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeMediator : Mediator {

    [Inject]
    public WelcomeView welcomeView { get; set; }

    /// <summary>
    /// 注册事件
    /// </summary>
    public override void OnRegister()
    {
        welcomeView.btn_play.onClick.AddListener(onPlayClick);
        welcomeView.btn_music.onClick.AddListener(onMusicClick);
        welcomeView.changeMusicState(SoundManager.Instance.mute);
    }

    /// <summary>
    /// 移除事件: 必须在Root的下面才能调用???
    /// </summary>
    public override void OnRemove()
    {
        welcomeView.btn_play.onClick.RemoveListener(onPlayClick);
        welcomeView.btn_music.onClick.RemoveListener(onMusicClick);
    }

    private void OnDestroy()
    {
        OnRemove();
    }

    private void onPlayClick() {
        SceneManager.LoadScene(2,LoadSceneMode.Single);
    }

    private void onMusicClick() {
        SoundManager.Instance.mute = !SoundManager.Instance.mute;
        welcomeView.changeMusicState(SoundManager.Instance.mute);
    }
}
