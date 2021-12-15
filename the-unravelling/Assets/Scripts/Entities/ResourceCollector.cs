using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceCollector : MonoBehaviour {
	[SerializeField]
	private List<GameObject> resources;

	private BaseUnit currentResource;

	private float damageTimer = 0;

	private float damageTimeout = 3f;

	public LineRenderer laser;

	public Transform laserStartPoint;
	void Start() {
		resources = new List<GameObject>();
	}

	void Update() {
		damageTimer += Time.deltaTime;

		if (currentResource == null) {
			if (resources.Count > 0) {
				currentResource = resources[0].GetComponent<BaseUnit>();
				drawLaserRay(laserStartPoint.position, resources[0].transform.position);
			}
		} else {
			if (damageTimer >= damageTimeout) {
				currentResource.OnDamage(20,true);
				StartCoroutine(toggleLaser());
				damageTimer = 0;
			}
		}
	}

	IEnumerator toggleLaser() {
		laser.startWidth = 0.2f;
		laser.endWidth = 0.2f;
		yield return new WaitForSeconds(0.1f);
		laser.startWidth = 0;
        laser.endWidth = 0;
	}

	void drawLaserRay(Vector2 startPos, Vector2 endPos) {
		laser.SetPosition(0,startPos);
		laser.SetPosition(1,endPos);
	}


	private void OnTriggerEnter2D(Collider2D other) {
		if (!resources.Contains(other.gameObject)) {
			if (other.CompareTag("Resource"))
				resources.Add(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		resources.Remove(other.gameObject);
	}
}
