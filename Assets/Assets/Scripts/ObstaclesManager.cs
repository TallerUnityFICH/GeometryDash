using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour {

	public GameObject blockPrefab;
	public GameObject spikePrefab;

	public LastObstacle lastObstacle = null;

	List<QueueEntry> queue = new List<QueueEntry>();

	int[] possibilities = new int[] {1, 1, 1, 2, 2, 3};
	GameObject obstacles;
	int spawnChance = 15;
	float offset = 0;
	int extraOffset = 0;

	void Start() {
		obstacles = GameObject.Find ("Obstacles");
		offset = (Screen.width > 720 ? (-Screen.width / 180f + 4) : 0.5f);
		if (offset < 0)
			offset = -offset;
	}

	void Update() {
		Flush ();

		float startX = blockPrefab.transform.localPosition.x + offset;
		if (lastObstacle == null || lastObstacle.obstacle == null || lastObstacle.obstacle.transform.localPosition.x + extraOffset < startX)
			if (Random.Range (1, 600) % 100 < spawnChance)
				Generate (startX);
			else
				extraOffset++;
	}

	void Flush() {
		float xTop = blockPrefab.transform.localPosition.x + offset;
		float startX = (lastObstacle == null || lastObstacle.obstacle == null) ?
			xTop : lastObstacle.obstacle.transform.localPosition.x;
		xTop += 6;

		QueueEntry[] clone = new QueueEntry[queue.Count];
		queue.CopyTo (clone);
		foreach (QueueEntry entry in clone)
			if (entry.x < xTop) {
				queue.Remove (entry);
				Spawn (entry.spike, startX, entry.x, entry.y, entry.height);
			}
	}

	void Generate(float startX) {
		if (spawnChance < 50)
			spawnChance += 3;

		int maxHeight = 2;

		if (lastObstacle != null && lastObstacle.obstacle != null) {
			startX = lastObstacle.obstacle.transform.localPosition.x;
			maxHeight += lastObstacle.height;

			if (lastObstacle.spike)
				maxHeight--;
		}

		startX += extraOffset;

		Cycle:

		if (maxHeight > 9)
			maxHeight = 9;

		int height = Random.Range (1, maxHeight);

		int floorHeight = height;
		if (lastObstacle != null && lastObstacle.obstacle != null)
			floorHeight -= lastObstacle.height;

		int maxDist = Mathf.FloorToInt(-floorHeight / 2 + 4);
		int minDist = (floorHeight < -1 || floorHeight > 0) ? 2 : 1;

		maxDist -= extraOffset;

		if (maxDist < 2) {
			maxHeight = 2;
			extraOffset = 0;
			goto Cycle;
		}

		int amp = Random.Range (minDist, maxDist);
		int width = 1;

		if (Random.Range (1, 600) % 2 == 0)
			width = Random.Range (2, Random.Range (1, 2) == 1 ? Random.Range (1, 2) == 2 ? 30 : 10 : 5);

		bool spikes = false;

		if (height < 3) {
			spikes = Random.Range (1, 5) < 3;
			if (spikes && (height < 2 || width > 1) && Random.Range (1, 7) < 3)
				height++;
		}

		for (int w = 0; w < width; w++)
			for (int h = 0; h < height; h++)
				if (spikes && width > 1 && (height > 1 || width > 3) && h == height - 1)
					if (w == width - 1)
						SpawnTopSpikes (startX, width, h);
					else
						continue;
				else
					Spawn (spikes && h == height - 1, startX + amp, w, h, height);

		extraOffset = 0;
	}

	public void SpawnObstacle(int amp, int width, int height, bool spikes = false) {
		float startX = (lastObstacle == null || lastObstacle.obstacle == null) ?
			blockPrefab.transform.localPosition.x + offset : lastObstacle.obstacle.transform.localPosition.x + amp;

		for (int w = 0; w < width; w++)
			for (int h = 0; h < height; h++)
				if (spikes && width > 1 && (height > 1 || width > 3) && h == height - 1)
					if (w == width - 1)
						SpawnTopSpikes (startX, width, h);
					else
						continue;
				else
					Spawn (spikes && h == height - 1, startX + amp, w, h, height);
	}

	void SpawnTopSpikes(float startX, int width, int h) {
		if (width == 2)
			Spawn (true, startX, 1, h, h + 1);
		else {

			bool first = true;
			int cap = 0, spikes = 0;

			for (int w = (h == 0 ? 0 : 1); w < width; w++) {
				if (spikes == 0)
					spikes = Random.Range (1, 4) == 1 ? (cap = possibilities[Random.Range (0, 6)]) : 0;
				
				if (spikes > 0) {
					Spawn (true, startX, w, h, h + 1);
					spikes--;
					if (spikes == 0)
						spikes = (first ? -2 : -1) - cap;
				} else if (spikes < 0)
					spikes++;

				first |= false;
			}
		}
	}

	void Spawn(bool spike, float x, int xOffset, int yOffset, int height) {
		if (x + xOffset > blockPrefab.transform.localPosition.x + offset + 8) {
			queue.Add(new QueueEntry(spike, xOffset, yOffset, height));
			return;
		}

		GameObject instance = Instantiate (spike ? spikePrefab : blockPrefab);

		instance.transform.parent = obstacles.transform;
		instance.transform.localPosition = new Vector3(x + xOffset, instance.transform.localPosition.y + yOffset, 0);
		instance.GetComponent<ObjectSlider> ().ready = true;

		lastObstacle = new LastObstacle(instance, height, spike);
	}
}

public class QueueEntry {

	public int x, y, height;
	public bool spike;

	public QueueEntry(bool spike, int x, int y, int height) {
		this.height = height;
		this.x = x;
		this.y = y;
		this.spike = spike;
	}
}

public class LastObstacle {
	public GameObject obstacle;
	public int height;
	public bool spike;

	public LastObstacle(GameObject lastObstacle, int height, bool spike) {
		obstacle = lastObstacle;
		this.height = height;
		this.spike = spike;
	}
}
