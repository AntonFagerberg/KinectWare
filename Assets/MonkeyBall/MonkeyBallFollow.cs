using UnityEngine;
using System.Collections;

public class MonkeyBallFollow : MonoBehaviour {

	public Transform ball;
	Vector3 distance = new Vector3(10f, 10f, 0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = ball.position + distance;
	}
}
