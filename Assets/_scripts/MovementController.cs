using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HardShellStudios.InputManagerV2;
using UnityEngine.Events;

public class MovementController : MonoBehaviour {

	public float horizontalMoveForce;
	public float jumpImpulse;

	[HideInInspector]
	public bool canMove = true;
	[HideInInspector]
	public UnityEvent onJump;

	private Vector3 checkpointPosition;
	private bool onGround = false;

	private Rigidbody myRigidbody;
	private BoxCollider myBoxCollider;

	// Use this for initialization
	private void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myBoxCollider = GetComponent<BoxCollider>();
		checkpointPosition = transform.position;

		GetComponent<Health>().onKill.AddListener(Revive);
	}

	public void Revive () {
		transform.position = checkpointPosition;

	}

	private void Update() {
		
		onGround = GetOnGround();

		if (hardInput.GetKeyDown("Jump") == true && onGround == true) {
			
			if (onJump != null) {
				onJump.Invoke();
			}
			//canMove = true;
			myRigidbody.AddForce(new Vector2(0, 1) * jumpImpulse, ForceMode.Impulse);

		} else if (hardInput.GetKeyUp("Jump") == true && myRigidbody.velocity.y > 0) {

			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.5f);

		}
	}

	private void FixedUpdate () {
		
		if (canMove == false) {
			return;
		}

		float h = hardInput.GetAxis("Right", "Left");
		
		if (h != 0) {
			myRigidbody.AddForce(new Vector2(1, 0) * h * horizontalMoveForce, ForceMode.Force);
		}
	}


	private bool GetOnGround() {

		int laymasks = (1 << GameManager.worldLayerMask) + (1 << GameManager.worldTableLayerMask);
		RaycastHit hit1, hit2;

		Physics.Raycast(myBoxCollider.bounds.center + new Vector3(-0.5f, 0), new Vector3(0, -0.6f), out hit1, 1, laymasks);
		if (hit1.collider != null) {
			return true;
		}

		Physics.Raycast(myBoxCollider.bounds.center + new Vector3(0.5f, 0), new Vector3(0, -0.6f), out hit2, 1, laymasks);
		if (hit2.collider != null) {
			return true;
		}

		return false;
	}
}
