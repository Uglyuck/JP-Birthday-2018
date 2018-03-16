using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour 
{
	public Sprite AliveHead;
	public Sprite DeadHead;
	public Sprite HappyHead;
	public SpriteRenderer ActiveHead;

	private delegate void StatePointer();
	private StatePointer currentState;
	private StatePointer HappySad;


	//public Transform groundCheck;
	private bool canJump = true;
	private bool InitialJump = false;
	//private float gravityForce = 2;
	//private Rigidbody2D rb2d;

	//private float JumpForce = 4;

	private float TimeInAir = 0f;
	private float TimeSad = 0f;
	private float TimeHappy = 0f;


	public AudioClip Yay;
	public AudioClip Boo;

	private void Start()
	{
		//rb2d = GetComponent<Rigidbody2D>();
		currentState = inAir;
		HappySad = Grounded;
	}
	// Update is called once per frame
	void Update () 
	{
		
		if(Input.GetMouseButtonDown(0) && canJump && TimeSad == 0f)
		{
		
			jump();
		}
		//bool grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//print(grounded);
		currentState();
		HappySad();
	}

	void inAir()
	{
		Vector2 newPosition = this.transform.position;
		if(TimeInAir < 0.3f)
		{
			newPosition.y += (30 * Time.deltaTime);
			//print("Im Jumping");
		}
		else
		{
			newPosition.y -= (15 * Time.deltaTime);
			//print("Im falling");
			//rb2d.gravityScale = 2f;
		}
		TimeInAir += Time.deltaTime;
		this.transform.position = newPosition;
	}
	void Grounded()
	{
		
	}
	void jump()
	{
		//GetComponent<Animator>().SetTrigger("Running");
		GetComponent<Animator>().SetBool("Jump", false);
		GetComponent<Animator>().SetTrigger("Jump");
		
		//GetComponent<Animator>().SetTrigger("Jump");
		if (InitialJump)
		{
			canJump = false;
		}
		InitialJump = true;
		//print("Im Jumping!");
		//rb2d.AddForce(new Vector2(0f, -2f));
		
		TimeInAir = 0;
		//rb2d.gravityScale = 0f;
		currentState = inAir;
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Ground"))
		{
			canJump = true;
			InitialJump = false;
			currentState = Grounded;
			//print("Frounded!");
		}
		//print("I collided with something!" + coll.gameObject.name);
		//rb2d.gravityScale = 2f;
		//rb2d.AddForce(new Vector2(0f, 12f));
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Ground") 
		{
			//gravityForce = 0;
		}
	}

	void Happy()
	{
		ActiveHead.sprite = AliveHead;
		TimeSad = 0f;
		TimeHappy = 0f;
		HappySad = Grounded;
	}

	void Sad()
	{
		TimeSad += Time.deltaTime;
		if(TimeSad > 2)
		{
			HappySad = Happy;

		}
	}

	void SuperHappy()
	{
		TimeHappy -= Time.deltaTime;
		if (TimeHappy < 0)
		{
			HappySad = Happy;

		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Bad Item"))
		{
			ActiveHead.sprite = DeadHead;
			HappySad = Sad;
			playAudioClip(Boo);
			//Destroy(collision.gameObject);
		}
		if (collision.CompareTag("Good Item"))
		{
			ActiveHead.sprite = HappyHead;
			HappySad = SuperHappy;
			playAudioClip(Yay);
			//Destroy(collision.gameObject);
			TimeSad = 0f;
			TimeHappy += 1;
		}
	}
	void playAudioClip(AudioClip play)
	{
		this.GetComponent<AudioSource>().clip = play;
		this.GetComponent<AudioSource>().Play();
	}
}
