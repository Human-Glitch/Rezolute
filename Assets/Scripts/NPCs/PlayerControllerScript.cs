using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public float maxSpeed = 10f;
	public float maxHeight = 10f;
	bool facingRight = true;

	Animator anim;
	private Vector2 movDir;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = .2f;
	public LayerMask whatIsGround;
	public float jumpForce = 70f;

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1f;
		anim = GetComponent<Animator> ();;
	}

	void Update()
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (grounded && Input.GetKey(KeyCode.Space))
		{
			anim.SetBool("Grounded", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("speed", Mathf.Abs (move)); //sets the controller to the animation
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y % maxHeight);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Flip ()
	{
		
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
