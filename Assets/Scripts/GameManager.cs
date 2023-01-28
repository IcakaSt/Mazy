using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	private Maze mazeInstance;

	private void Start () {
		BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private void BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
	}

	private void RestartGame () {
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}