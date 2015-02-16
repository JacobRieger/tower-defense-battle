using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float Speed = 10;
	public float Range = 10;

	private float _distanceTraveled;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * Speed);
		_distanceTraveled += Time.deltaTime * Speed;
		if(_distanceTraveled >= Range)
		{
			Destroy(gameObject);
		}
	}
}
