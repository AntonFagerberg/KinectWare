using UnityEngine;
using System.Collections;

public class ArkPaddle : MonoBehaviour {
	public Object block;
	public Transform head;

	private float speed = 0.2f;

	void Start () {
		for (int j = 0; j < 8; j++) {
			for (int i = 0; i <= 16; i++) {
				GameObject newBlock = (GameObject) Instantiate(block, new Vector3(-7.5f + i, 5.5f - 0.5f * j, 0f), Quaternion.identity);
				if (j % 2 == 0) {
					newBlock.renderer.material.color = (i % 2 == 0) ? Color.white : Color.gray;
				} else {
					newBlock.renderer.material.color = (i % 2 != 0) ? Color.white : Color.gray;
				}

			}
		}
	}
	
	void Update () {
		transform.position = new Vector3(head.position.x * 30, transform.position.y, transform.position.z);
		
		if (transform.position.x > 7.5f) {
			transform.position = new Vector3(7.5f, transform.position.y, transform.position.z);
		} else if (transform.position.x < -7.5f) {
			transform.position = new Vector3(-7.5f, transform.position.y, transform.position.z);
		}
	}
}