using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

	public List<string> uneffectedByFactions;
	public float maxHP;
	public bool aimable = true;
	public string particleSystemExplosion = "Debris";

	public UnityEvent onDamage;
	public UnityEvent onKill;

	private float hp;

	// Use this for initialization
	private void Start () {
		hp = maxHP;

		if (aimable == true) {

			GameManager.shootableObjects.Add(transform);
		}
	}
	
	public void TakeDamage (string faction, float damage) {

		// uneffectedByFactions
		for (int i = 0; i < uneffectedByFactions.Count; i += 1) {
			if (faction == uneffectedByFactions[i]) {
				return;
			}
		}

		hp -= damage;

		if (hp < 0) {
			onKill.Invoke();
			GameManager.shootableObjects.Remove(transform);
			
			if (particleSystemExplosion == "Debris") {
				GameManager.debrisParticleSystem.transform.position = transform.position;
				GameManager.debrisParticleSystem.Play();
			}

			if (GetComponent<DestroyObject>()) {
				GetComponent<DestroyObject>().ExecuteDestruction();
			}
			return;
		}

		onDamage.Invoke();
	}
}
