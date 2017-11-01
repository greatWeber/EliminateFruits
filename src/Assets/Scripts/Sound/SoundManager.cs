using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音乐管理
/// </summary>

//当没有对象这个组件时，会加载这个组件
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    private static SoundManager _instance;

    public static SoundManager Instance {
        get { return _instance; }
    }

    public string pathDir;

    private void Awake()
    {
        _instance = this;
        GetComponent();
        bgSource.loop = true;
        bgSource.playOnAwake = false;
        mute = PlayerPrefs.GetInt("mute") == 0? false: true;
    }

    void GetComponent() {
        bgSource = this.GetComponent<AudioSource>();
    }

    #region 背景音乐
    private AudioSource bgSource;

    //是否静音
    public bool mute {
        get { return bgSource.mute; }
        set {
            bgSource.mute = value;
            //保存静音数据
            PlayerPrefs.SetInt("mute",value?1:0);

        }
    }

    //音量大小
    public float volume
    {
        get { return bgSource.volume; }
        set { bgSource.volume = value; }
    }

    public void onPlayBGM(string path) {
        
        string name = pathDir + "/" + path;
        if (ResManager.Instance.LoadPrefab(name) != null) {
            AudioClip clip = ResManager.Instance.LoadPrefab(name) as AudioClip;
            bgSource.clip = clip;
            bgSource.Play();
        }
        
    }

    public void onStopBGM() {
        bgSource.clip = null;
        bgSource.Stop();
    }

    #endregion

    //音效
    public void onPlayClip(string path, Vector2 pos = new Vector2()) {
        if (mute)
            return;
        string name = pathDir + "/" + path;
       
        if (ResManager.Instance.LoadPrefab(name) != null) {
            AudioClip clip = ResManager.Instance.LoadPrefab(name) as AudioClip;
            AudioSource.PlayClipAtPoint(clip, pos);
        }
        
    }
}
