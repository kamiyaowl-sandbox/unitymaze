using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Maze {
	public enum Piece { Empty, Wall, Start, Goal, Answer, NoAnswer, Mark, Hint }
	public Piece[][] Field { get; set; }
	public int Width { get { return width; } }
	public int Height { get { return height; } }

	private int width;
	private int height;
	public Maze(int w, int h) {
		width = w;
		height = h;
		Field = new Piece[height][];
		for (int i = 0; i < height; ++i) {
			Field[i] = new Piece[width];
		}
	}
	public void Create(MazeCreator creator) {
		creator.Create(width, height, Field);
	}
	public Maze Solve(MazeSolver solver) {
		var ans = new Maze(width, height);
		ans.Field = solver.Solve(width, height, CopyField(width, height, Field));
		return ans;
	}


	public static Maze.Piece[][] CopyField(int w, int h, Maze.Piece[][] field) {
		var clone = new Maze.Piece[h][];
		for (int j = 0; j < h; ++j) {
			clone[j] = new Maze.Piece[w];
			for (int i = 0; i < w; ++i) {
				clone[j][i] = field[j][i];
			}
		}
		return clone;
	}

}
