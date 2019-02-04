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

	void Awake(){
		playerTrans = transform;
		playerRB = GetComponent<Rigidbody2D> ();
	}

	public void Start(){
		isGround = true;
		checkGround = false;
	}


	void Update () {
		if(!isGround && checkGround){
			if(GetCollisionUnderPlayer()) isGround = true; //接地していたらisGroundをtrueにする
		}

		if(canInput){
			float dir = Input.GetAxisRaw("Horizontal"); //左右キーの入力を取得（左キーは-1、右キーは1）
			if (Mathf.Abs(dir) > 0) //左右キーの入力があったら移動処理
				PlayerMove (dir);
			if (Input.GetKeyDown (KeyCode.UpArrow)) //上キーの入力があったらジャンプ処理
				PlayerJump ();
		}
	}

	void FixedUpdate(){
		playerRB.AddForce(Vector2.down * gravityForce); //追加の重力
	}

	void PlayerMove(float dir){
		playerTrans.position += Vector3.right * dir * playerMoveSpeed * Time.deltaTime; //Playerの座標 += 右ベクトル * 方向(-1 or 1) * 移動速度 * 前フレームからの時間
	}

	void PlayerJump(){
		if (isGround) {
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
}
