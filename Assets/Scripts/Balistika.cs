using System;
using UnityEngine;

public class Balistika : MonoBehaviour
{
	[Serializable] public class InitTrajectory
	{
		public float Y;
		public float X;
	}
	[SerializeField] private InitTrajectory initTrajectory;

	[Serializable] public class ChangeTrajectory
	{
		public float SpeedChangeTrajectoryX;
		public float SpeedChangeTrajectoryY;
	}
	[SerializeField] private ChangeTrajectory changeTrajectory;

	[Serializable] public class LimitTrajectory
	{
		public float LimitTop;
		public float LimitBottom;
	}
	[SerializeField] private LimitTrajectory limitTrajectory;

	[SerializeField] private KeyCode keyCodeBalistika;
	private Rigidbody rigidbody;

	[SerializeField] private TrajectoryRenderer trajectoryRenderer;
	[SerializeField] private bool mayPush;
	public bool MayPush 
	{
		get { return mayPush; } 
		set { mayPush = value; }
	}

	private float valueHolding;
	private bool mayDecrease = false;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		valueHolding = initTrajectory.Y;
		trajectoryRenderer.gameObject.SetActive(false);
		
	}

	void Update()
	{
		if (!mayPush) return;
		
		if (Input.GetKey(keyCodeBalistika))
		{
			if (valueHolding < limitTrajectory.LimitTop && !mayDecrease)
			{
				valueHolding += Time.deltaTime * changeTrajectory.SpeedChangeTrajectoryY;
				initTrajectory.X -= Time.deltaTime * changeTrajectory.SpeedChangeTrajectoryX;
				initTrajectory.Y = valueHolding;
			}
			else mayDecrease = true;

			if (mayDecrease)
			{
				if (valueHolding > limitTrajectory.LimitBottom)
				{
					valueHolding -= Time.deltaTime * changeTrajectory.SpeedChangeTrajectoryY;
					initTrajectory.X += Time.deltaTime * changeTrajectory.SpeedChangeTrajectoryX;
					initTrajectory.Y = valueHolding;
				}
				else mayDecrease = false;
			}
			trajectoryRenderer.gameObject.SetActive(true);
			Vector3 speed = new Vector3(0, initTrajectory.Y, initTrajectory.X);
			trajectoryRenderer.ShowTrajectory(transform.position, speed);
		}
		if (Input.GetKeyUp(keyCodeBalistika))
		{
			rigidbody.AddForce(new Vector3(0, initTrajectory.Y, initTrajectory.X), ForceMode.Impulse);
			rigidbody.useGravity = true;
			trajectoryRenderer.gameObject.SetActive(false);
		}
	}
}