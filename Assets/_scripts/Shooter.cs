using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	public float shootDistance = 10f;
	public float weaponDamage = 10f;

	private Transform target;
	private Transform myTransform;
	private Transform lightningEnd;
	private LineRenderer aimLineRenderer;

	private	List<Transform> sortedTargets = new List<Transform>();

	// Use this for initialization
	private void Start () {
		myTransform = transform;
		lightningEnd = transform.Find("LightningLine").transform.Find("LightningEnd");
		aimLineRenderer = transform.Find("AimLine").GetComponent<LineRenderer>();

		sortedTargets = GameManager.shootableObjects;
	}

	private void SetTarget() {


		sortedTargets.Sort(delegate (Transform a, Transform b) {
			return Vector2.Distance(myTransform.position, a.position)
			.CompareTo(Vector2.Distance(myTransform.position, b.position));
		});
		
		if (sortedTargets.Count > 0
			&& target != sortedTargets[0]
			&& Vector2.Distance(transform.position, sortedTargets[0].position) < shootDistance) {

			target = sortedTargets[0];
		}

		/*
		for (int i = 0; i < GameManager.shootableObject.Count; i += 1) {
			if (target != GameManager.shootableObject[i]
				&& Vector2.Distance(transform.position, GameManager.shootableObject[i].position) < shootDistance) {

				target = GameManager.shootableObject[i];
				aimLineRenderer.SetPosition(1, transform.InverseTransformPoint(target.position));
			}
		}
		*/
	}
	IEnumerator ResetEndPosition () {

		GetComponent<SpriteRenderer>().color = Color.red;

		yield return new WaitForSeconds(0.3f);

		GetComponent<SpriteRenderer>().color = Color.white;

		lightningEnd.position = new Vector3(myTransform.position.x, myTransform.position.y, -0.6f);
	}

	private void Update () {

		// set target
		SetTarget();

		// Aim line
		if (sortedTargets.Count == 0 ||
			(sortedTargets.Count >= 0 && Vector2.Distance(transform.position, sortedTargets[0].position) > shootDistance)) {
			aimLineRenderer.SetPosition(1, transform.InverseTransformPoint(myTransform.position));
		} else {

			aimLineRenderer.SetPosition(1, transform.InverseTransformPoint(target.position));
		}


		if (hardInput.GetKeyDown("Shoot") == true && GameManager.powerInLevel > 0 && target != null) {
			
			RaycastHit hit;
			Physics.Linecast(myTransform.position, target.position, out hit, (1 << GameManager.worldLayerMask) + (1 << GameManager.enemyLayerMask));
			
			if (hit.collider != null) {

				if (hit.collider.GetComponent<Health>()) {
					hit.collider.GetComponent<Health>().TakeDamage("LightningOrb", weaponDamage);
				}

				lightningEnd.position = new Vector3(hit.point.x, hit.point.y, -1);
				StartCoroutine(ResetEndPosition());
			}
		}
	}

}
