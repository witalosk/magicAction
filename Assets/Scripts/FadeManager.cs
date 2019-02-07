using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * FadeManager.FadeIn(フェード時間, 色(黒は0、白は1), フェード後に実行するメソッド(省略化));
 * FadeManager.FadeOutも同様
 */

public class FadeManager : MonoBehaviour {

    static float time;
    static bool fadeFlag;
    static Image fadeImg;
    static float startAlpha;
    static float endAlpha;
    static float fadeTime;
    static Action action;

    static bool isUnscaledTime;

    // [SerializeField] bool onAwake;

    void Awake(){
        time = 0;
        fadeFlag = false;
        fadeImg = GetComponent<Image> ();
        SetAlpha (1f);
    }

    public static void FadeIn(float fadeTime, int color, Action action){
        FadeStart (1f, 0f, fadeTime, action, color, false);
    }
    public static void FadeIn(float fadeTime, int color){
        FadeStart (1f, 0f, fadeTime, null, color, false);
    }   
    public static void FadeIn(float fadeTime, int color, Action action, bool isUnscaledTime){
        FadeStart (1f, 0f, fadeTime, action, color, true);
    }
    public static void FadeIn(float fadeTime, int color, bool isUnscaledTime){
        FadeStart (1f, 0f, fadeTime, null, color, true);
    }
    public static void FadeOut(float fadeTime, int color, Action action){
        FadeStart (0f, 1f, fadeTime, action, color, false);
    }
    public static void FadeOut(float fadeTime, int color){
        FadeStart (0f, 1f, fadeTime, null, color, false);
    }
        public static void FadeOut(float fadeTime, int color, Action action, bool isUnscaledTime){
        FadeStart (0f, 1f, fadeTime, action, color, true);
    }
    public static void FadeOut(float fadeTime, int color, bool isUnscaledTime){
        FadeStart (0f, 1f, fadeTime, null, color, true);
    }


    //color=0のとき黒、color=1のとき白
    public static void FadeStart(float _startAlpha, float _endAlpha, float _fadeTime, Action _action, int color, bool _isUnScaledTime){
        startAlpha = _startAlpha;
        endAlpha = _endAlpha;
        fadeTime = _fadeTime;
        action = _action;
        isUnscaledTime = _isUnScaledTime;

        fadeImg.color = new Color (color, color, color, startAlpha);
        time = 0;
        fadeFlag = true;
    }

    void Update(){
        if (fadeFlag) {
            float deltaTime = (isUnscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime; //unscaledDeltaTimeはTime.timeScale=0でも動作する
            time += deltaTime / fadeTime;
            SetAlpha (Mathf.Lerp (startAlpha, endAlpha, time));
            if (time >= 1f) {
                fadeFlag = false;
                if(action != null)
                    action.Invoke ();
            }
        }
    }

    static void SetAlpha(float a){
        Color tmp = fadeImg.color;
        tmp.a = a;
        fadeImg.color = tmp;
    }
}

