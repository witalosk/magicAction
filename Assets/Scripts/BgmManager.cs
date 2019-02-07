using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * BgmManager.FadeIn(音源(インスペクタで指定), 最終的なVolume, フェード時間);
 * フェードアウトは未実装
 */

public class BgmManager : MonoBehaviour {

    public static BgmManager Instance {
        get; private set;
    }

    static AudioSource bgmAS;
    [SerializeField] AudioClip[] clips;

    static bool playStart = false;
    static int clipNo;

    static float startVol;
    static float endVol;
    static float fadeTime;
    static bool fade = false;

    float t;

    void Awake(){
        if (Instance != null) {
            Destroy (gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad (gameObject);

        bgmAS = GetComponent<AudioSource> ();
    }

    public static void BGMPlay(int clip, float vol){
        if (bgmAS.isPlaying && bgmAS.volume == vol) {
            return;
        }
        
        clipNo = clip;
        bgmAS.volume = vol;
        playStart = true;
    }

    public static void BGMStop(){
        bgmAS.Stop();
    }

    public static void BGMVol(float vol, float t){
        startVol = bgmAS.volume;
        endVol = vol;
        fadeTime = t;
        t = 0;
        fade = true;
    }

    public static void BGMFadeIn(int clip, float vol, float t){
        BGMPlay (clip, 0f);
        BGMVol (vol, t);
    }

    void Update(){
        if (playStart) {
            bgmAS.clip = clips [clipNo];
            bgmAS.Play ();
            playStart = false;
        }

        if (fade) {
            if (fadeTime <= 0) {
                t = 1f;
            } else {
                t += Time.deltaTime / fadeTime;
            }
            bgmAS.volume = Mathf.Lerp (startVol, endVol, t);
            if (t >= 1f) {
                fade = false;
            }
        }
    }


}

