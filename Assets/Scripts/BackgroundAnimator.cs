using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimator : MonoBehaviour
{
	public Sprite[] sprites;
	public float speed = 6;
	public bool loop = true;
	public bool destroyOnEnd = false;

	private int index = 0;
	private Image image;
	private int frame = 0;
	private float timeOnFrame;

	void Awake()
	{
		image = GetComponent<Image>();
		timeOnFrame = Time.time + speed;
	}

	void Update()
	{
		if (!loop && index == sprites.Length) return;
		frame++;
		if (Time.time < timeOnFrame) return;
		image.sprite = sprites[index];
		frame = 0;
		timeOnFrame = Time.time + speed;
		index++;
		if (index >= sprites.Length)
		{
			if (loop) index = 0;
			if (destroyOnEnd) Destroy(gameObject);
		}
	}
}
