using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Note: there's no winning, or losing in this because
//		 It's his birthday... You can't win, you have already won!
public class PlayerInput : MonoBehaviour 
{
	// Collection of the various graphics involved.
	public Sprite AliveHead;
	public Sprite DeadHead;
	public Sprite HappyHead;
	public SpriteRenderer ActiveHead;

	// State Pointers
	private delegate void StatePointer();	
	private StatePointer currentState;		// This is a pointer to a function for the physical state.
	private StatePointer HappySad;          // This is a pointer to a function for the Emotional state counters.

	// State variables
	private bool canJump = true;    // This is to measure the double jump. 1 bit...  (Less memory on phone.)
	private bool InitialJump = false;	// This is to identify the second jump. 1 bit... (Less memory on phone.)

	private float TimeInAir = 0f;			// This is so I can fake my gravity real quickly.
	private float TimeSad = 0f;				// This is to time his "shitty"-ness when he gets shit.
	private float TimeHappy = 0f;			// This is to time his overhappyness for when he gets a gift.


	public AudioClip Yay;	// For when he gets the gift
	public AudioClip Boo;   // For when he gets shit

	private void Start()
	{
		currentState = inAir;
		HappySad = Grounded;
	}
	// Update is called once per frame
	void Update () 
	{
		// If he's sad he doesn't jump... Also if they tap he jumps, And possibility for double jumping.
		if(Input.GetMouseButtonDown(0) && canJump && TimeSad == 0f)
		{
		
			jump();
		}
		// Call the physical state
		currentState();
		// Call the emotional state for time counters.
		HappySad();
	}


	// State Controllers: Physical states:
	void inAir()
	{
		Vector2 newPosition = this.transform.position;
		if(TimeInAir < 0.3f)
		{
			newPosition.y += (30 * Time.deltaTime);
		}
		else
		{
			newPosition.y -= (15 * Time.deltaTime);
		}
		TimeInAir += Time.deltaTime;

		// Could add collision detection here by calculating a specific point for himn to fall on... But not much is gained....

		this.transform.position = newPosition;
	}

	// Required for physical state controller.
	void Grounded()
	{	
	}

	void jump()
	{
		GetComponent<Animator>().SetBool("Jump", false);
		GetComponent<Animator>().SetTrigger("Jump");
		if (InitialJump)
		{
			canJump = false;
		}
		InitialJump = true;
		TimeInAir = 0;
		currentState = inAir;
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Ground"))
		{
			canJump = true;
			InitialJump = false;
			currentState = Grounded;
		}
	}

	// State controller, Emotional states... Will always go back to regular happy face.
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

	// Interact with other objects! How this will interact! 
	// If this was larger I'd create multiple objects in code and have them call the Player component then have individual funcations for various effects.
	// But since it's so small it's simple enough to just compare tags.
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Bad Item"))
		{
			ActiveHead.sprite = DeadHead;
			HappySad = Sad;
			playAudioClip(Boo);
		}
		if (collision.CompareTag("Good Item"))
		{
			ActiveHead.sprite = HappyHead;
			HappySad = SuperHappy;
			playAudioClip(Yay);
			TimeSad = 0f;
			TimeHappy += 1;
		}
	}

	// This doesn't work with iphone and doesn't work with itch.io at all
	void playAudioClip(AudioClip play)
	{
		this.GetComponent<AudioSource>().clip = play;
		this.GetComponent<AudioSource>().Play();
	}
}
