using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour {

	public GameObject blockPrefab;
	public GameObject spikePrefab;

	public LastObstacle lastObstacle = null;

	GameObject obstacles;

	void Start() {
		obstacles = GameObject.Find ("Obstacles");
	}

	public void SpawnObstacle(int amp, int width, int height, bool spikes = false) {
		float startX = (lastObstacle == null || lastObstacle.obstacle == null) ?
			blockPrefab.transform.localPosition.x : lastObstacle.obstacle.transform.localPosition.x + amp;

		for (int w = 0; w < width; w++)
			for (int h = 0; h < height; h++)
				Spawn (spikes && h == height - 1, startX + w, h, height);

	}

	void Spawn(bool spike, float x, float yOffset, int height) {
		GameObject instance = Instantiate (spike ? spikePrefab : blockPrefab);

		instance.transform.parent = obstacles.transform;
		instance.transform.localPosition = new Vector3(x, instance.transform.position.y + yOffset, 0);
		instance.GetComponent<ObjectSlider> ().ready = true;

		lastObstacle = new LastObstacle(instance, height);
	}
}

public class LastObstacle {
	public GameObject obstacle;
	public int height;

	public LastObstacle(GameObject lastObstacle, int height) {
		obstacle = lastObstacle;
		this.height = height;
	}
}
