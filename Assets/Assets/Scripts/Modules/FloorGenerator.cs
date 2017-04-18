using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

	public GameObject floorPrefab;

	List<GameObject> floorCache = new List<GameObject>();
	List<GameObject> usedFloors = new List<GameObject>();

	Color color = new Color(1, 1, 1, 0.6f);
	GameObject lastFloor;
	float width;

	public void DisableFloor(GameObject floor) {
		floor.SetActive (false);
		usedFloors.Remove (floor);
		floorCache.Add (floor);

		Generate ();
	}

	public void updateColor(Color color) {
		this.color = color;

		foreach (GameObject obj in floorCache)
			obj.transform.GetComponent<SpriteRenderer> ().color = color;
		
		foreach (GameObject obj in usedFloors)
			obj.transform.GetComponent<SpriteRenderer> ().color = color;
	}

	void Start () {
		GameObject prefab = Instantiate (floorPrefab);
		prefab.transform.parent = transform;

		usedFloors.Add (prefab);

		lastFloor = prefab;
		width = floorPrefab.GetComponent<SpriteRenderer> ().bounds.size.x;

		Generate ();
	}
	
	void Generate() {
		GameObject floor = GetFloor ();

		Vector3 position = floor.transform.position;
		position.x = lastFloor.transform.position.x + width;
		floor.transform.position = position;

		lastFloor = floor;
	}

	GameObject GetFloor() {
		if (floorCache.Count < 1) {
			GameObject prefab = Instantiate (floorPrefab);
			prefab.GetComponent<SpriteRenderer> ().color = color;
			prefab.transform.parent = transform;
			usedFloors.Add (prefab);
			return prefab;
		} else {
			GameObject prefab = floorCache[0];
			floorCache.Remove (prefab);
			prefab.SetActive(true);
			usedFloors.Add (prefab);
			return prefab;
		}
	}
}
