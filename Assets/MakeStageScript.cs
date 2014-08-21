using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeStageScript : MonoBehaviour {
	public Transform StartPrefab;
	public Transform GoalPrefab;
	public Transform WallPrefab;
	public Transform GuidePrefab;
	public Transform HintPrefab;
	public float WallHeight = 1.5f;
	public int Width = 10;
	public int Height = 10;
	public bool IsHinted = false;

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
		const float size = 0.5f;
		WallPrefab.transform.localScale = new Vector3(1.0f, WallHeight, 1.0f);
		StartPrefab.transform.localScale = new Vector3(size, WallHeight, size);
		GoalPrefab.transform.localScale = new Vector3(size, WallHeight, size);
		mazeObject = new Dictionary<Maze.Piece, Transform>(){
			{Maze.Piece.Start,StartPrefab},
			{Maze.Piece.Goal,GoalPrefab},
			{Maze.Piece.Wall,WallPrefab},
			{Maze.Piece.Answer,GuidePrefab},
			{Maze.Piece.Hint,HintPrefab},
		};
	}

	public void Make() {
		//偶数構成の迷路は事故る
		if (Width % 2 == 0) Width++;
		if (Height % 2 == 0) Height++;
		transform.localScale = new Vector3(Width / 10.0f, 1, Height / 10.0f);

		/* Maze Init */
		maze = new Maze(Width, Height);
		maze.Create(new SimpleMazeCreator());
		/* Make Hint */
		var hx = Random.Range(2, Width - 3);
		var hy = Random.Range(2, Height - 3);
		maze.Field[hy][hx] = Maze.Piece.Hint;
		
		for (int j = 0; j < Height; ++j) {
			for (int i = 0; i < Width; ++i) {
				if (mazeObject.ContainsKey(maze.Field[j][i])) {
					var pos = new Vector3(i - Width / 2, WallHeight / 2.0f, j - Height / 2);
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
		Debug.Log(string.Format("{0} * {1} maze Created\n[{2},{3}] is hint", Width, Height, hx, hy));
	}
	public void MakeHint() {
		if (!IsHinted) {
			var ansMaze = maze.Solve(new SimpleMazeSolver());
			for (int j = 0; j < Height; ++j) {
				for (int i = 0; i < Width; ++i) {
					if (ansMaze.Field[j][i] == Maze.Piece.Answer) {
						var pos = new Vector3(i - Width / 2, WallHeight / 2.0f, j - Height / 2);
						Instantiate(mazeObject[ansMaze.Field[j][i]], pos, new Quaternion());
					}
				}
			}
			Debug.Log(string.Format("{0} * {1} maze hint Created", Width, Height));
		}
		IsHinted = true;
	}
	// Update is called once per frame
	void Update() {

	}

}
