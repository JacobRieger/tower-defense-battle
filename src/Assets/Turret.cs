using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public Transform Target;
	public GameObject Projectile;
	public float ReloadTime = 1f;
	public float FirePauseTime = .25f;
	public float TurnSpeed = 5f;
	public GameObject LauchEffect;
	public float ErrorAmount = .001f;
	public Transform[] projectileLaunchLocations;
	public Transform Launcher;

	public float _nextFireTime;
	public float _aimError;
	public float _nextMoveTime;
	public Quaternion _desiredRotation;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Target)
		{
			if(Time.time >= _nextMoveTime)
			{
				CalculateAimPosition(Target.position);
				Launcher.rotation = Quaternion.Lerp(Launcher.rotation, _desiredRotation, Time.deltaTime * TurnSpeed);
			}
			Debug.Log("Time.time = " + Time.time.ToString());
			Debug.Log("Next Fire = " + _nextFireTime.ToString());
			if(Time.time >= _nextFireTime)
			{
				FireProjectile();
			}
		}
	}

	void OnTriggerEnter(Collider enteringObject)
	{
		Debug.Log("Trigger Entered");
		if(enteringObject.gameObject.tag == "Enemy")
		{
			Debug.Log ("Enemy Entered range");
			_nextFireTime = Time.time + (ReloadTime * .5f);
			Target = enteringObject.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider exitingObject)
	{
		if(exitingObject.gameObject.transform == Target)
		{
			Target = null;
		}
	}

	void FireProjectile()
	{
		Debug.Log ("Fire Projectile");
		_nextFireTime = Time.time + ReloadTime;
		_nextMoveTime = Time.time + FirePauseTime;
		CalculateAimError();

		foreach(var launchLocation in projectileLaunchLocations)
		{
			Instantiate (Projectile, launchLocation.position, launchLocation.rotation);
			Instantiate (LauchEffect, launchLocation.position, launchLocation.rotation);
		}
	}

	void CalculateAimPosition(Vector3 targetPosition)
	{
		Debug.Log ("Calculate Aim Position");
		var aimPoint = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z + _aimError);
		_desiredRotation = Quaternion.LookRotation(aimPoint);
	}

	void CalculateAimError()
	{
		_aimError = Random.Range (-ErrorAmount, ErrorAmount);
	}

}
