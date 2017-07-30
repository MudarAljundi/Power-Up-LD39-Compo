using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour {

	private bool playerOnTreadmill = false;

	// Use this for initialization
	private void Start () {
		GameManager.playerTransform.GetComponent<MovementController>().onJump.AddListener(StopTreadmill);
		GameManager.playerTransform.GetComponent<Health>().onKill.AddListener(StopTreadmill);
	}

	private void StopTreadmill () {

		GameManager.playerTransform.GetComponent<MovementController>().canMove = true;
		playerOnTreadmill = false;
	}
	private void OnTriggerEnter (Collider collider) {
		if (collider.name == "Player") {

			GameManager.playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0);
			GameManager.playerTransform.GetComponent<MovementController>().canMove = false;
			GameManager.playerTransform.position = transform.position;

			playerOnTreadmill = true;
		}
	}
	private void Update() {
		
		if (playerOnTreadmill == true) {

			if (hardInput.GetKey("Right")) {

				GetComponent<Animator>().SetInteger("Direction", 1);
				GameManager.powerInLevel += 30 * Time.deltaTime;
			}
			else if (hardInput.GetKey("Left")) {

				GetComponent<Animator>().SetInteger("Direction", -1);
				GameManager.powerInLevel += 30 * Time.deltaTime;
			} else {

				GetComponent<Animator>().SetInteger("Direction", 0);
			}

		} else {

			GetComponent<Animator>().SetInteger("Direction", 0);
		}
	}
}
