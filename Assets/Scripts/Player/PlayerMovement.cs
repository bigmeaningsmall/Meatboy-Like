using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private bool freezePlayer = false;

	public float maxSpeed = 8;
	public float acceleration = 20;
	public float jumpSpeed = 10;
	public float jumpDuration;
	
	public bool enableDoubleJump = true;
	public bool wallHitDoubleJumpOverride = true;

	//internal checks
	bool canDoubleJump = true;
	float jumpDurationInternal;
	bool jumpKeyDown = false;
	bool canVariableJump = false;

	private float horizontal;

	[HideInInspector]
	public bool facingRight = true;
	public bool facingLeft = false;

	private SpriteRenderer thisRenderer;
	private Rigidbody2D thisRigidBody;
	private Transform spriteTransform;

	public Animator animator;
	private float playerSpeed;

	void Start () {
		thisRenderer = gameObject.GetComponent<SpriteRenderer> ();
		thisRigidBody = gameObject.GetComponent<Rigidbody2D> ();
		spriteTransform = gameObject.GetComponent<Transform> ();
	}

	void OnEnable(){
		TriggerWin.OnLevelComplete += TriggerWin_OnLevelComplete;
	}
	void OnDisable(){
		TriggerWin.OnLevelComplete -= TriggerWin_OnLevelComplete;
	}
	void TriggerWin_OnLevelComplete (){		
		freezePlayer = true;
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	void Update () {
		if (!freezePlayer) {
			horizontal = Input.GetAxis ("Horizontal");
		}

		if (horizontal < -0.1f) {
			if(thisRigidBody.velocity.x > -this.maxSpeed){
				thisRigidBody.AddForce(new Vector2(-this.acceleration, 0.0f));
				facingLeft = true;
				facingRight = false;
				FaceLeft();
			}
			else{
				thisRigidBody.velocity = new Vector2(-this.maxSpeed, thisRigidBody.velocity.y);
			}
		}
		else if(horizontal > 0.1f) {
			if(thisRigidBody.velocity.x < this.maxSpeed){
				thisRigidBody.AddForce(new Vector2(this.acceleration, 0.0f));
				facingLeft = false;
				facingRight = true;
				FaceRight();
			}
			else{
				thisRigidBody.velocity = new Vector2(this.maxSpeed, thisRigidBody.velocity.y);
			}
		}

		bool onTheGround = isOnGround();

		
		if (onTheGround) {
			canDoubleJump = true;
		}


		if (Input.GetButtonDown("Jump") && !freezePlayer){
			if(!jumpKeyDown){
				jumpKeyDown = true;
				
				if(onTheGround || (canDoubleJump && enableDoubleJump) || wallHitDoubleJumpOverride){
					bool wallHit = false;
					int wallHitDirection = 0;
					
					bool leftWallHit = isOnWallLeft();
					bool rightWallHit = isOnWallRight();

					if(horizontal != 0){
						if(leftWallHit){
							wallHit = true;
							wallHitDirection = 1;
						}
						else if(rightWallHit){
							wallHit = true;
							wallHitDirection = -1;
						}
					}
					if(!wallHit){
						if(onTheGround || (canDoubleJump && enableDoubleJump)){
							thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, this.jumpSpeed);
							jumpDurationInternal = 0.0f;
							canVariableJump = true;

							//find the audiomanager and play the sound
							if (AudioManager.instance != null) {
								AudioManager.instance.PlayJump();
							}
						}
					}
					else{
						thisRigidBody.velocity = new Vector2(this.jumpSpeed * wallHitDirection, this.jumpSpeed);
						jumpDurationInternal = 0.0f;
						canVariableJump = true;

						//find the audiomanager and play the sound
						if (AudioManager.instance != null) {
							AudioManager.instance.PlayJump();
						}
					}

					if(!onTheGround && !wallHit){
						canDoubleJump = false;
					}
				}
			}
			//2nd frame
			else if(canVariableJump){
				jumpDuration += Time.deltaTime;
				if(jumpDuration < this.jumpDurationInternal /1000){
					thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, this.jumpSpeed);

				}
			}
		}
		else{
			jumpKeyDown = false;
			canVariableJump = false;


		}


		//animation parameters
		if (isOnGround ()) {
			Debug.Log ("GROUNDED");
			animator.SetBool ("jump", false);
		} else {
			Debug.Log ("JUMNPING");
			animator.SetBool ("jump", true);
		}

		playerSpeed = Mathf.Abs (thisRigidBody.velocity.x);
		animator.SetFloat ("run", playerSpeed);

		if (playerSpeed > 0.1f || playerSpeed < -0.1f) {
			if (isOnGround ()) {
				Debug.Log ("RUNNING ON GROUND");
			} 
		}
	}


	public void FaceLeft(){
		spriteTransform.localScale = new Vector3 (-1, spriteTransform.localScale.y, spriteTransform.localScale.z);
	}
	public void FaceRight(){
		spriteTransform.localScale = new Vector3 (1, spriteTransform.localScale.y, spriteTransform.localScale.z);
	}

	private bool isOnGround(){
		bool retVal = false;
		float lengthToSearch = 0.1f;
		float colliderThreshold = 0.001f;

		//create a line from the renderer boundry
		Vector2 lineStart = new Vector2 (this.transform.position.x, this.transform.position.y - this.thisRenderer.bounds.extents.y - colliderThreshold);
		//define a line search length
		Vector2 vectorToSearch = new Vector3 (this.transform.position.x, lineStart.y - lengthToSearch);
		//draw a raycast
		RaycastHit2D hit = Physics2D.Linecast (lineStart, vectorToSearch);

		retVal = hit;
//		if (retVal) {
//			if(hit.collider.GetComponent<Bullet>()){
//				retVal = false;
//				return retVal;
//			}
//		}


		//return a hit
		return hit;
	}

	private bool isOnWallLeft(){
		bool retVal = false;
		float lengthToSearch = 0.1f;
		float colliderThreshold = 0.01f;

		//create a line from the renderer boundry
		Vector2 lineStart = new Vector2 (this.transform.position.x - this.thisRenderer.bounds.extents.x - colliderThreshold, this.transform.position.y);
		//define a line search length
		Vector2 vectorToSearch = new Vector2 (lineStart.x - lengthToSearch, this.transform.position.y);
		//draw a raycast
		RaycastHit2D hitLeft = Physics2D.Linecast (lineStart, vectorToSearch);
		//set hit bool to the hit raycast
		retVal = hitLeft;
		//if the wall collider has NoSlideJumpComponent return false
		if (retVal) {
			if(hitLeft.collider.GetComponent<NoSlideJump>()){
				retVal = false;
			}
		}
		return retVal;
	}
	private bool isOnWallRight(){
		bool retVal = false;
		float lengthToSearch = 0.1f;
		float colliderThreshold = 0.01f;

		//create a line from the renderer boundry
		Vector2 lineStart = new Vector2 (this.transform.position.x + this.thisRenderer.bounds.extents.x + colliderThreshold, this.transform.position.y);
		//define a line search length
		Vector2 vectorToSearch = new Vector2 (lineStart.x + lengthToSearch, this.transform.position.y);
		//draw a raycast
		RaycastHit2D hitRight = Physics2D.Linecast (lineStart, vectorToSearch);
		//set hit bool to the hit raycast
		retVal = hitRight;
		//if the wall collider has NoSlideJumpComponent return false
		if (retVal) {
			if(hitRight.collider.GetComponent<NoSlideJump>()){
				retVal = false;
			}
		}
		return retVal;
	}

}
