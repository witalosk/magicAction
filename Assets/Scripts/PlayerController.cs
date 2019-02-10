using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  Transform playerTrans;
  Rigidbody2D playerRB;

  [SerializeField] float playerMoveSpeed; //移動速度
  [SerializeField] float jumpPower; //ジャンプ力

  [SerializeField] float gravityForce; //追加の重力（これがないと落下に時間がかかり、ゲームっぽくない）

  [SerializeField] float checkGroundRayLength; //着地しているかを検知するときに確認する長さ（地面にギリギリ当たる長さにする）

  bool isGround; //接地しているか
  bool checkGround; //接地判定をするか（ジャンプ直後はしない）

  public bool canInput; //入力を受け付けるか

  float actionRayLengthForward = 1.0f; //ActionTriggerを検知するRayの前方向の長さ
  float actionRayLengthBack = 0.5f; //ActionTriggerを検知するRayの後ろ方向の長さ

  // アニメーション用
  private Animator animator;

  void Awake(){
    playerTrans = transform;
    playerRB = GetComponent<Rigidbody2D> ();
  }

  public void Start(){
    isGround = true;
    checkGround = false;
    
    animator = GetComponentInChildren<Animator>(); // アニメータの取得
  }


  void Update () {
    if(!isGround && checkGround){
      if(GetCollisionUnderPlayer()) isGround = true; //接地していたらisGroundをtrueにする
    }

    animator.SetBool("is_running", false);

    if(canInput){
      float dir = Input.GetAxisRaw("Horizontal"); //左右キーの入力を取得（左キーは-1、右キーは1）
      if (Mathf.Abs(dir) > 0) //左右キーの入力があったら移動処理
        PlayerMove (dir);
      if (Input.GetKeyDown (KeyCode.UpArrow)) //上キーの入力があったらジャンプ処理
        PlayerJump ();
      if (Input.GetKeyDown (KeyCode.Space)) //スペースキーの入力があったらアクション処理
        PlayerAction ();
    }
  }

  void FixedUpdate(){
    playerRB.AddForce(Vector2.down * gravityForce); //追加の重力
  }

  void PlayerMove(float dir){
    animator.SetBool("is_running", true);
    playerTrans.position += Vector3.right * dir * playerMoveSpeed * Time.deltaTime; //Playerの座標 += 右ベクトル * 方向(-1 or 1) * 移動速度 * 前フレームからの時間
  
    // プレーヤーの描画向き変更
    if (dir > 0) playerTrans.localScale = new Vector3(2.0f, 2.0f, 2.0f);
    else playerTrans.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
  }

  void PlayerJump(){
    if (isGround) {
      animator.SetTrigger("is_jumping");
      playerRB.velocity = new Vector3(playerRB.velocity.x, 0, 0); //一度垂直方向の加速度をリセット
      playerRB.AddForce (Vector2.up * jumpPower); //上方向にjumpPower分の力を加える
      isGround = false;
      checkGround = false; //このタイミングで接地判定すると既に接地していると誤判定が起こりやすいため↓
      Invoke("EnableCheckGround", 0.2f); //0.2秒後に接地判定を開始する
    }
  }

  void EnableCheckGround(){
    checkGround = true; //接地判定開始
  }

  public RaycastHit2D GetCollisionUnderPlayer(){
    //Playerの足元にColliderがあるかを判定（接地判定）  最後の引数はレイヤー9(Playerレイヤー)以外を対象とする、という意味
    RaycastHit2D hit = Physics2D.Raycast (playerTrans.position, Vector2.down, checkGroundRayLength, ~(1 << 9));
    return hit;
  }

  void PlayerAction(){
    RaycastHit2D[] hits = GetActionTrigger();
    foreach (RaycastHit2D hit in hits){
      if(hit.transform.GetComponent<ActionTrigger>()){
        hit.transform.GetComponent<ActionTrigger>().ExecuteAction();
      }
    }
  }

  RaycastHit2D[] GetActionTrigger(){
    //Playerの正面にActionTriggerがあるかを判定  最後の引数はレイヤー10(ActionTriggerレイヤー)を対象とする、という意味
    RaycastHit2D[] hits = Physics2D.RaycastAll (
      playerTrans.position - playerTrans.right * actionRayLengthBack, 
      playerTrans.right, 
      actionRayLengthBack + actionRayLengthForward, 
      1 << 10);
    return hits;
  }

}

