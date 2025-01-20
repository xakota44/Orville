using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	public Sprite[] frontIdleRightSprites;
	public Sprite[] frontIdleLeftSprites;
	public Sprite[] backIdleRightSprites;
	public Sprite[] backIdleLeftSprites;
	public Sprite[] frontWalkRightSprites;
	public Sprite[] frontWalkLeftSprites;
	public Sprite[] backWalkRightSprites;
	public Sprite[] backWalkLeftSprites;
	public Sprite[] backRollLeftSprites;
	public Sprite[] backRollRightSprites;
	public Sprite[] frontRollLeftSprites;
	public Sprite[] frontRollRightSprites;
	public float speed = 0.1f;
	public bool loop = true;
	public bool destroyOnEnd = false;

	private int index = 0;
	private SpriteRenderer sprite;
	private int frame = 0;
	private float timeOnFrame;
	private Sprite[] sprites;
	private bool rightFlag;
	private bool upFlag;
	private bool rolling;
	private bool firstRoll;
	public PlayerController playerController;
	void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
		timeOnFrame = Time.time + speed;
		sprites = frontIdleRightSprites;
		playerController = GetComponent<PlayerController>();

	}

	IEnumerator StartRollCounter(float time)
	{
		
		float rollTime = time;
		for (int i = 0; i < (time * 1000); i++)
		{
			while (rollTime >= 0)
			{
				rollTime -= Time.smoothDeltaTime;
				rolling = true;
				yield return null;
			}
		}
		rolling = false;
	}

	void Update()
	{
		
		if (!rolling)
		{
			firstRoll = false;
			if (Input.GetKey(KeyCode.D)) //right
			{
				if (upFlag)
				{
					sprites = backWalkRightSprites;
				}
				else
				{
					sprites = frontWalkRightSprites;
				}

				rightFlag = true;
			}
			if (Input.GetKey(KeyCode.A)) //left
			{
				if (upFlag)
				{
					sprites = backWalkLeftSprites;
				}
				else
				{
					sprites = frontWalkLeftSprites;
				}

				rightFlag = false;
			}
			if (Input.GetKey(KeyCode.W)) //up
			{
				if (rightFlag)
				{
					sprites = backWalkRightSprites;
				}
				else
				{
					sprites = backWalkLeftSprites;
				}

				upFlag = true;
			}
			if (Input.GetKey(KeyCode.S)) //down
			{
				if (rightFlag)
				{
					sprites = frontWalkRightSprites;
				}
				else
				{
					sprites = frontWalkLeftSprites;
				}

				upFlag = false;
			}
			if (!Input.anyKey)
			{
				if (upFlag && rightFlag)
				{
					sprites = backIdleRightSprites;
				}
				if (upFlag && !rightFlag)
				{
					sprites = backIdleLeftSprites;
				}
				if (!upFlag && rightFlag)
				{
					sprites = frontIdleRightSprites;
				}
				if (!upFlag && !rightFlag)
				{
					sprites = frontIdleLeftSprites;
				}
			}
	
			if (!loop && index == sprites.Length) return;
			frame++;
			if (Time.time < timeOnFrame) return;
			if (index >= sprites.Length)
			{
				index = 0;
			}
			sprite.sprite = sprites[index];
			frame = 0;
			timeOnFrame = Time.time + speed;
			index++;
			if (index >= sprites.Length)
			{
				if (loop) index = 0;
				if (destroyOnEnd) Destroy(gameObject);
			}
		}
		else
		{
			if (!firstRoll)
            {
				index = 0;
				firstRoll = true;
            }
			//if (!loop && index == sprites.Length) return;
			if (Time.time < timeOnFrame) return;
			if (index <= 8) //9 frames has to be hardcoded?
            {
				sprite.sprite = sprites[index];
			}
			
			//frame = 0;
			timeOnFrame = Time.time + speed;
			
			if (index >= 8)//9 frames has to be hardcoded?
			{
				
				StopCoroutine(StartRollCounter(playerController.GetRollLength()));
				rolling = false;
				index = 0;
			}
			else
            {
				index++;
			}
		}
		

		
		

	}
	public void Roll(Vector3 direction, float time)
    {
		Debug.Log("roll animation");

		if (direction == Vector3.up)
		{
			if (rightFlag)
            {
				sprites = backRollRightSprites;
			}
			else
            {
				sprites = backRollLeftSprites;
			}
			

		}
		if (direction == Vector3.down)
		{
			if (rightFlag)
			{
				sprites = frontRollRightSprites;
			}
			else
			{
				sprites = frontRollLeftSprites;
			}
		}
		if (direction == Vector3.left)
		{
			if (upFlag)
			{
				sprites = backRollLeftSprites;
			}
			else
			{
				sprites = frontRollLeftSprites;
			}
		}
		if (direction == Vector3.right)
		{
			if (upFlag)
			{
				sprites = backRollRightSprites;
			}
			else
			{
				sprites = frontRollRightSprites;
			}
		}
		StartCoroutine(StartRollCounter(time));

		
		
	}
	private void rollAnimation(float time)
    {
		float rollFrameTime = Time.time + (time / 9); //9 frames for roll  
		Debug.Log(sprites.Length);
		if (!loop && index == sprites.Length) return;
		frame++;
		if (Time.time < rollFrameTime) return;
		if (index >= sprites.Length)
		{
			index = 0;
		}
		sprite.sprite = sprites[index];
		frame = 0;
		rollFrameTime = Time.time + (time / 9);
		index++;
		if (index >= sprites.Length)
		{
			if (loop) index = 0;
			if (destroyOnEnd) Destroy(gameObject);
		}
	}
}
