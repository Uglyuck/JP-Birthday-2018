using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDrivenV2 : MonoBehaviour
{


	// DDv2 had the following
	// (Added Vector2 and removed all references to Transform.
	// Worst out of small test:
	// Frame: 380: 1.71ms
	//		MoveOBjects = 1.71ms (2 instances)
	//		SetPosition = 0.728ms (504 Instances)

	// GameLoop had the following:
	// Frame: 245: 2.84ms
	//		MoveObjects = 2.81ms (2 instances)
	//		TransformTranslate = 1.89ms total (1500 instances)
	//		GetTransform = 0.373ms total 1003 instances 




	private Vector2[] Poops_v = new Vector2[50];
	private GameObject[] Poops = new GameObject[50];
	private int Poops_LastActive = 0;

	private Vector2[] Gifts_v = new Vector2[500];
	private GameObject[] Gifts = new GameObject[500];
	private int Gifts_LastActive = 500;

	public float CharacterHitValue = 0;
	public float ObjectSpeed = 15;



	public GameObject MainCharacter;
	private float Velocity = 0;
	private float MainCharacter_Y = 0;
	private float Gravity = -23f;
	private ushort JumpCount = 0;
	private bool isEmotion = false;
	private bool isSad = false;
	private float EmotionTime = 0;



	private Sprite DeadHead;
	private AudioClip Boo;   // For when he gets shit
	private Sprite HappyHead;
	private AudioClip Yay;   // For when he gets the gift
	private SpriteRenderer ActiveHead;
	private Sprite AliveHead;





	private float TotalTime = 0;
	private int Cycles = 0;

	// Use this for initialization
	void Start()
	{
		//Load all the presets
		GameObject Gift = Resources.Load<GameObject>("Gift");
		GameObject Poo = Resources.Load<GameObject>("Shit");
		AliveHead = Resources.Load<Sprite>("Head1");
		DeadHead = Resources.Load<Sprite>("Head2");
		HappyHead = Resources.Load<Sprite>("Head3");
		ActiveHead = GameObject.Find("Head").GetComponent<SpriteRenderer>();
		Yay = Resources.Load<AudioClip>("Yay");
		Boo = Resources.Load<AudioClip>("Fart");
		if (MainCharacter == null)
		{
			MainCharacter = GameObject.Find("Johnathan");
		}

		//Populate the arrays and gameboard.
		float Distance = 30;
		for (int x = 0; x < Gifts.Length; x++)
		{
			Gifts[x] = Instantiate(Gift);
			Gifts_v[x] = new Vector2(Distance += 30, (((x % 3) * (int)(x / 2)) * 7) - 3);
			Gifts[x].transform.position = Gifts_v[x];
		}
		for (int x = 0; x < Poops.Length; x++)
		{
			Poops_v[x] = new Vector2(0f,0f);
			Poops[x] = Instantiate(Poo);
			Poops[x].active = false;
		}

		MainCharacter_Y = MainCharacter.transform.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();

		//-1 - -6 hit
		float deltaTime = Time.deltaTime;

		//Calculate from characeter Position

		MoveCharacter(deltaTime);

		//float CharacterHitValue = 0;
		// Calculate Hit Areas
		if (MainCharacter_Y < 1.5)
		{
			CharacterHitValue = -3;
		}
		else if (MainCharacter_Y < 8)
		{
			CharacterHitValue = 4;
		}
		else
		{
			CharacterHitValue = 11;
		}



		if (isEmotion)
		{
			EmotionTime -= deltaTime;
			if (EmotionTime <= 0)
			{
				MakeNutural();
			}
		}



		// Move Items Around
		if (wasHit_MoveObjects(CharacterHitValue, deltaTime, Poops, Poops_v, Poops_LastActive))
		{
			MakeSad();
		}
		if (wasHit_MoveObjects(CharacterHitValue, deltaTime, Gifts, Gifts_v, Gifts_LastActive))
		{
			MakeHappy();
		}

		timer.Stop();
		System.TimeSpan ts = timer.Elapsed;
		TotalTime += timer.ElapsedTicks;
		Cycles++;
		if (Cycles % 120 == 0)
		{
			print("Average Ticks:" + TotalTime / Cycles);
			TotalTime = 0;
			Cycles = 0;
		}
		//print("Milliseconds: " + timer.ElapsedTicks);

	}

	void MoveCharacter(float deltaTime)
	{

		// Jump Logic
		if (!isSad)
		{
			if (Input.GetMouseButtonDown(0) && JumpCount < 2)
			{
				Velocity = Gravity * -.7f;
				JumpCount++;
			}
		}

		// Gravity Physics // -1.6 ground?
		if (MainCharacter_Y != -1.16)
		{
			Velocity = Velocity + (Gravity * deltaTime);
			MainCharacter_Y += (Velocity * deltaTime);

			if (MainCharacter_Y < -1.16)
			{
				MainCharacter_Y = -1.16f;
				JumpCount = 0;
				Velocity = 0;
			}
			MainCharacter.transform.position = new Vector2(-4f, MainCharacter_Y);
		}

	}


	bool wasHit_MoveObjects(float CharacterHitValue, float deltaTime, GameObject[] ScreenBoards, Vector2[] ScreenBoards_v, float ScreenBoards_LastActive)
	{
		bool GotHit = false;
		//Move ObjectsAround
		for (int x = 0; x < ScreenBoards_LastActive; x++)
		{
			ScreenBoards_v[x].x -= (ObjectSpeed * deltaTime);
			if (ScreenBoards_v[x].x < -1)
			{
				if (ScreenBoards_v[x].x > -6)
				{
					if (ScreenBoards_v[x].y == CharacterHitValue)
					{
						ScreenBoards_v[x] = new Vector2(100, (Random.RandomRange((int)0, (int)3) * 7) - 3);
						activateObject(new Vector2(Random.RandomRange((int)50, (int)75) * 2, (Random.RandomRange((int)0, (int)3) * 7) - 3));
						GotHit = true;
					}
				}
				else if (ScreenBoards_v[x].x < -11)
				{
					ScreenBoards_v[x] = new Vector2(Random.RandomRange((int)50, (int)75) * 2, (Random.RandomRange((int)0, (int)3) * 7) - 3);
				}
			}
			ScreenBoards[x].transform.position = ScreenBoards_v[x];
		}
		return GotHit;
	}


	void activateObject(Vector2 NewPosition)//, GameObject NewObject)
	{
		try
		{
			Poops_v[Poops_LastActive] = NewPosition;
			Poops[Poops_LastActive].active = true;// = NewObject;
			Poops[Poops_LastActive].transform.position = NewPosition;
			Poops_LastActive++;
		}
		catch (System.Exception e)
		{
		}
	}



	//Emotional Statuses...
	void MakeNutural()
	{
		isEmotion = false;
		isSad = false;

		ActiveHead.sprite = AliveHead;
	}
	void MakeHappy()
	{
		isEmotion = true;
		isSad = false;
		EmotionTime = 3;

		ActiveHead.sprite = HappyHead;
		playAudioClip(Yay);
	}
	void MakeSad()
	{
		isEmotion = true;
		isSad = true;
		EmotionTime = 5;

		ActiveHead.sprite = DeadHead;
		playAudioClip(Boo);
	}

	void playAudioClip(AudioClip play)
	{
		this.GetComponent<AudioSource>().clip = play;
		this.GetComponent<AudioSource>().Play();
	}


	/*
	void reactivateObject(GameObject go, float x, float y)
	{
		go.transform.position = new Vector2(x , y);
	}
	*/
}
