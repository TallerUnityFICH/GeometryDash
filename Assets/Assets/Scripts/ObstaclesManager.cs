using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour {

	public GameObject blockPrefab;
	public GameObject spikePrefab;

	public LastObstacle lastObstacle = null;

	int[] possibilities = new int[] {1, 1, 1, 2, 2, 3};
	GameObject obstacles;

	void Start() {
		obstacles = GameObject.Find ("Obstacles");
	}

	void Update() {
		flush ();

		float startX = blockPrefab.transform.localPosition.x;
		if (lastObstacle == null || lastObstacle.obstacle == null || lastObstacle.obstacle.transform.position.x < startX - 1)
			generate (startX);
	}

	void flush() {
	}

	void generate(float startX) {
	}

	public void SpawnObstacle(int amp, int width, int height, bool spikes = false) {
		float startX = (lastObstacle == null || lastObstacle.obstacle == null) ?
			blockPrefab.transform.localPosition.x : lastObstacle.obstacle.transform.localPosition.x + amp;

		for (int w = 0; w < width; w++)
			for (int h = 0; h < height; h++)
				if (spikes && width > 1 && (height > 1 || width > 3) && h == height - 1)
					if (w == width - 1)
						SpawnTopSpikes (startX, width, h);
					else
						continue;
				else
					Spawn (spikes && h == height - 1, startX + w, h, height);
	}

	void SpawnTopSpikes(float startX, int width, int h) {
		if (width == 2)
			Spawn (true, startX + 1, h, h + 1);
		else {

			bool first = true;
			int cap = 0, spikes = 0;

			for (int w = (h == 0 ? 0 : 1); w < width; w++) {
				if (spikes == 0)
					spikes = Random.Range (1, 4) == 1 ? (cap = possibilities[Random.Range (0, 6)]) : 0;
				
				if (spikes > 0) {
					Spawn (true, startX + w, h, h + 1);
					spikes--;
					if (spikes == 0)
						spikes = (first ? -2 : -1) - cap;
				} else if (spikes < 0)
					spikes++;

				first |= false;
			}
		}
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
