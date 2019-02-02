using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Transform playerTrans;
	Rigidbody2D playerRB;

	[SerializeField] float playerMoveSpeed;
	[SerializeField] float jumpPower;

	[SerializeField] float gravityForce;

	[SerializeField] LayerMask groundCheckLM;

	bool isGround;
	bool checkGround;

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
			RaycastHit2D hit = Physics2D.Raycast (playerTrans.position, Vector2.down, 1.2f, groundCheckLM);
			if(hit) isGround = true;
		}

		float dir = Input.GetAxisRaw("Horizontal");
		if (Mathf.Abs(dir) > 0.01f)
			PlayerMove (dir);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			PlayerJump ();
	}

	void FixedUpdate(){
		playerRB.AddForce(Vector2.down * gravityForce);
	}

	void PlayerMove(float dir){
		playerTrans.position += Vector3.right * dir * playerMoveSpeed * Time.deltaTime;
	}

	void PlayerJump(){
		if (isGround) {
			playerRB.velocity = new Vector3(playerRB.velocity.x, 0, 0);
			playerRB.AddForce (Vector2.up * jumpPower);
			isGround = false;
			checkGround = false;
			Invoke("EnableCheckGround", 0.2f);
		}
	}

	void EnableCheckGround(){
		checkGround = true;
	}
}
