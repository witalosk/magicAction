using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour {

    Transform bgParentTrans; //自分自身のTransform
    Transform cameraTrans;
    Transform playerTrans;

    [SerializeField] float speed; //0だとPlayerと一緒に移動、1だとPlayerの動きに関係なく固定
    Vector3 prePos;


    Transform[] bgTranses; //背景画像3枚のTransform
    float spriteWidth; //背景画像の幅
    int centerSpriteIdx; //現在真ん中にある画像のインデックス(0~2)

    void Awake(){
        bgParentTrans = GetComponentInChildren<Transform>();
        bgTranses = new Transform[3];
        for(int i = 0; i < 3; i++)
            bgTranses[i] = bgParentTrans.GetChild(i);
//         spriteWidth = bgTranses[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
		spriteWidth = bgParentTrans.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        centerSpriteIdx = 1; //1からスタート
    }

    void Start(){
        playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
        prePos = playerTrans.position;
    }

    //毎Update()の直後に呼び出される関数
    void LateUpdate(){ 
        bgParentTrans.position += Vector3.right * (playerTrans.position.x - prePos.x) * speed; //親をPlayerに合わせて移動
        prePos = playerTrans.position;

        CheckSeamless();
    }

    //連続的に見せるための処理
    void CheckSeamless(){
        float diff = playerTrans.position.x - bgTranses[centerSpriteIdx].position.x; //現在真ん中にある背景画像のx座標とPlayerのx座標との距離
        if(diff > spriteWidth / 2){ //Playerが右の背景画像に差し掛かった場合、左の背景画像を一番右に移動し、centerSpriteIdxを更新
            bgTranses[(centerSpriteIdx + 2) % 3].position += Vector3.right * spriteWidth * 3;
            centerSpriteIdx = (centerSpriteIdx + 1) % 3;
        }else if(diff < -spriteWidth / 2){ //逆も同様
            bgTranses[(centerSpriteIdx + 1) % 3].position -= Vector3.right * spriteWidth * 3;
            centerSpriteIdx = (centerSpriteIdx + 2) % 3;
        }
    }
}

