using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager global;
	public static bool simulate = true;

	public GameObject obstacles;

	ObstaclesManager oManager;
	//float respawnPoint;

	void Start () {
		global = this;
		oManager = obstacles.GetComponent<ObstaclesManager> ();
		//respawnPoint = GameObject.Find ("MapEnd").transform.localPosition.x + 10;
	}

	void Update () {
		if (oManager.lastObstacle == null || oManager.lastObstacle.obstacle == null)
			oManager.SpawnObstacle (0, 4, 2);
		//else if (oManager.lastObstacle.obstacle.transform.localPosition.x < respawnPoint) {
		//	oManager.SpawnObstacle (Random.Range (1, 10), Random.Range (1, oManager.lastObstacle.height + 2), Random.Range (1, 4), false);
		//}
	}

	public void GameOver() {
		simulate = false;
		GetComponent<PlayerController> ().anim.Stop ();
		print ("GameOver");
	}
}
