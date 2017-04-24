using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {

	Animator anim;

	public float moveSpeed;
	public bool moveRight;

	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;

	private bool notAtEdge;
	private Vector2 movDir;
	public Transform edgeCheck;

	// Use this for initialization
	void Start () {
		movDir = GetComponent<Rigidbody2D> ().velocity;
	}
	
	// Update is called once per frame
	void Update () 
	{
		hittingWall = notAtEdge = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		notAtEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);

		if (hittingWall || !notAtEdge) {
			moveRight = !moveRight;
		}

		if (moveRight) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			//var movDir = GetComponent<Rigidbody2D> ().velocity;
			movDir = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);
			//var movDir = GetComponent<Rigidbody2D> ().velocity;
			movDir = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody2D> ().MovePosition ((Vector2)(GetComponent<Rigidbody2D> ().position) 
			+ (Vector2)(transform.TransformDirection (movDir) * moveSpeed * Time.deltaTime));	
	}

}
