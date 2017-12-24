using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour {


	public float ZrotationSpeed;
	public float YrotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,YrotationSpeed,ZrotationSpeed);
	}
}
