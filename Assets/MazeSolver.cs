using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
abstract class MazeSolver {
	public abstract Maze.Piece[][] Solve(int w, int h, Maze.Piece[][] field);
}
class SimpleMazeSolver : MazeSolver {

	public override Maze.Piece[][] Solve(int w, int h, Maze.Piece[][] field) {
		int sx = 0, sy = 0;//start x,y
		bool isFound = false;
		for (int j = 0; j < h && !isFound; ++j) {
			for (int i = 0; i < w && !isFound; ++i) {
				if (field[j][i] == Maze.Piece.Start) {
					sx = i;
					sy = j;
					isFound = true;
				}
			}
		}
		findGoal(w, h, sx, sy, field);
		field[sy][sx] = Maze.Piece.Start;
		return field;
	}

	private bool findGoal(int w, int h, int x, int y, Maze.Piece[][] field) {

		switch (field[y][x]) {
			case Maze.Piece.Goal:
				return true;
			case Maze.Piece.Start:
			case Maze.Piece.Empty:
				field[y][x] = Maze.Piece.Mark;
				var challenge = new int[][] { 
						new int[]{ x - 1, y }, 
						new int[]{ x + 1, y }, 
						new int[]{ x, y - 1 }, 
						new int[]{ x, y + 1 }
					};
				foreach (var pos in challenge) {
					if (findGoal(w, h, pos[0], pos[1], field)) {
						field[y][x] = Maze.Piece.Answer;
						return true;
					}
				}
				//全部ダメ
				field[y][x] = Maze.Piece.NoAnswer;
				return false;
			default:
				return false;
		}
	}
}
