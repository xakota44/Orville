using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Start is called before the first frame update
	public PlayerAnimationController animationController;
	public float speed = 1;
	private float constantSpeed;
	public float altWalkSpeed = 8;
	public float pushSpeed = 8;
	public float rollSpeed = 40;
	public float rollDelay = 5;
	public float rollLength = 1f;
	private bool rollCooldown;
	private Vector3 localPos;
	private Vector3 point;
	private Vector3 direction;
	private Vector3 rollDirection;
	void Start()
    {
		constantSpeed = speed;
		
	}
	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

		rollCooldown = false;
	}
	IEnumerator StartRollCounter(Vector3 Rolldir)
	{
		float rollTime = rollLength;
		for (int i = 0; i < (rollLength * 1000); i++)
		{
			while (rollTime >= 0)
			{
				rollTime -= Time.smoothDeltaTime;
				transform.position += Rolldir * rollSpeed * Time.deltaTime;
				yield return null;
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
			rollDirection = Vector3.right;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
			rollDirection = Vector3.left;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
			rollDirection = Vector3.up;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
			rollDirection = Vector3.down;
		}
		if (Input.GetKey(KeyCode.Space))
		{
			if(!rollCooldown)
            {
				Roll(rollDirection);
			}
			
		}
		if (Input.GetKey(KeyCode.LeftShift) && speed != altWalkSpeed)
		{
			speed = altWalkSpeed;
		}
        if(!Input.GetKey(KeyCode.LeftShift))
        {
			speed = constantSpeed;
        }

		point.Set(0, 0, 0);
		direction.Set(0, -1, 0);
		float angle = 0;
		localPos = transform.position;
		Vector3 fromPlayerToPoint = point - localPos;
		Vector3 pop = Vector3.ProjectOnPlane(fromPlayerToPoint, transform.up);
		float pointOrientation = Vector3.SignedAngle(transform.right, fromPlayerToPoint, transform.up);

		if (pointOrientation <= 90)
			angle = Vector3.SignedAngle(direction, fromPlayerToPoint, transform.up);
		else
			angle = (Vector3.SignedAngle(direction, fromPlayerToPoint, transform.up) * -1) + 360;
		var pushDir = Quaternion.Euler(0, 0, angle) * Vector2.left;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = pushDir * pushSpeed;

	}
	private void Roll(Vector3 rollDirection)
    {

		//Rigidbody2D rb = GetComponent<Rigidbody2D>();
		//rb.velocity = rollDirection * rollSpeed;
		StartCoroutine(StartRollCounter(rollDirection));
		StartCoroutine(ExecuteAfterTime(rollDelay));
		Debug.Log("roll");
		rollCooldown = true;
		

		//Get script attached to it
		animationController = GetComponent<PlayerAnimationController>();

		//Call the function
		animationController.Roll(rollDirection, rollLength);
	}
	public float GetRollLength()
    {
		return rollLength;

	}
	

}
