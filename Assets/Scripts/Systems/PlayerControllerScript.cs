using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour 
{
	public float maxSpeed = 10f;
	public float maxHeight = 10f;
	public float jumpForce = 70f;

	private Vector2 movDir;
	private bool facingRight = true;
	public bool grounded = false;
	private float groundRadius = .2f;
	public bool isMoving;

	Animator anim;

	public Transform groundCheck;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () 
	{
		isMoving = false;
		Time.timeScale = 1f;
		anim = GetComponent<Animator> ();;
	}
		
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (grounded && Input.GetKey(KeyCode.Space))
		{
			anim.SetBool("Grounded", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}

		//Detect grounded and set animation bool based on raycast results
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);//set vertical speed

		float move = Input.GetAxis ("Horizontal");

		//Keep track of motion bool
		if(Mathf.Abs(move) > 0 || grounded == false){isMoving = true; }
		else if(grounded == true && move == 0) {isMoving = false;}

		anim.SetFloat ("speed", Mathf.Abs (move)); //sets the controller to the animation
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y % maxHeight);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}//end late update

	private void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool detectPlayerMovement()
	{
		return isMoving;
	}

	public bool detectIfGrounded()
	{
		return grounded;
	}
}//end class
