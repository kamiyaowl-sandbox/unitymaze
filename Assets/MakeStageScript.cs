using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeStageScript : MonoBehaviour {
	public Transform StartPrefab;
	public Transform GoalPrefab;
	public Transform WallPrefab;
	public Transform GuidePrefab;
	public float WallHeight = 1.5f;

	private int width;
	private int height;
	private Texture[] textures;
	private Texture useTexture;
	private Maze maze;
	private Dictionary<Maze.Piece, Transform> mazeObject;
	// Use this for initialization
	void Start() {
		/* Resources Load */
		textures = new Texture[]{
			Resources.Load("unitychan_tile3") as Texture,
			Resources.Load("unitychan_tile4") as Texture,
			Resources.Load("unitychan_tile5") as Texture,
			Resources.Load("unitychan_tile6") as Texture,
		};
		useTexture = textures.TakeRandom();

		renderer.material.mainTexture = useTexture;
		WallPrefab.renderer.material.mainTexture = useTexture;
		WallPrefab.transform.localScale = new Vector3(1.0f, WallHeight, 1.0f);
		StartPrefab.transform.localScale = new Vector3(1.0f, WallHeight, 1.0f);
		GoalPrefab.transform.localScale = new Vector3(1.0f, WallHeight, 1.0f);
		/* Field Init */
		width = Random.Range(10, 40);
		height = Random.Range(10, 40);
		transform.localScale = new Vector3(width / 10.0f, 1, height / 10.0f);

		/* Maze Init */
		maze = new Maze(width, height);
		maze.Create(new SimpleMazeCreator());
		maze = maze.Solve(new SimpleMazeSolver());
		mazeObject = new Dictionary<Maze.Piece, Transform>(){
			{Maze.Piece.Start,StartPrefab},
			{Maze.Piece.Goal,GoalPrefab},
			{Maze.Piece.Wall,WallPrefab},
			{Maze.Piece.Answer,GuidePrefab},
		};
		for (int j = 0; j < height; ++j) {
			for (int i = 0; i < width; ++i) {
				if (mazeObject.ContainsKey(maze.Field[j][i])) {
					var pos = new Vector3(i - width / 2, WallHeight / 2.0f, j - height / 2);
					Instantiate(mazeObject[maze.Field[j][i]], pos, new Quaternion());
					if (maze.Field[j][i] == Maze.Piece.Start) {
						/* Move player to Start */
						var player = GameObject.FindGameObjectWithTag("Player");
						player.transform.position = pos;
					}
				}
			}
		}
		/* Debug */
		Debug.Log(string.Format("{0} * {1} maze Created"));
	}

	// Update is called once per frame
	void Update() {

	}

}
