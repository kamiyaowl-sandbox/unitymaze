using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract class MazeCreator {
	public abstract void Create(int w, int h, Maze.Piece[][] field);
	public void WallAround(int w, int h, Maze.Piece[][] field) {
		for (int i = 0; i < w; ++i) {
			field[0][i] = Maze.Piece.Wall;
			field[h - 1][i] = Maze.Piece.Wall;
		}
		for (int j = 0; j < h; ++j) {
			field[j][0] = Maze.Piece.Wall;
			field[j][w - 1] = Maze.Piece.Wall;
		}
	}
	public void FillAll(int w, int h, Maze.Piece[][] field, Maze.Piece piece) {
		for (int j = 0; j < h; ++j) {
			for (int i = 0; i < w; ++i) {
				if (field[j][i] == Maze.Piece.Start || field[j][i] == Maze.Piece.Goal) continue;
				field[j][i] = piece;
			}
		}
	}
}
/// <summary>
/// 棒倒し法で迷路を生成します
/// </summary>
class SimpleMazeCreator : MazeCreator {
	public override void Create(int w, int h, Maze.Piece[][] field) {
		this.WallAround(w, h, field);

		for (int j = 2; j < h - 1; j += 2) {
			for (int i = 2; i < w - 1; i += 2) {
				field[j][i] = Maze.Piece.Wall;
				//棒倒し
				int x = i, y = j;
				RandomUtil.Call(() => x -= 1, () => x += 1, () => y += 1);
				field[y][x] = Maze.Piece.Wall;
			}
		}
		field[1][1] = Maze.Piece.Start;
		field[h - 2][w - 2] = Maze.Piece.Goal;
	}
}
class DigMazeCreator : MazeCreator {
	public override void Create(int w, int h, Maze.Piece[][] field) {
		FillAll(w, h, field, Maze.Piece.Wall);
		int sx = RandomUtil.Range(1, w - 2);
		int sy = RandomUtil.Range(1, h - 2);
		digWall(w, h, sx, sy, field);
	}

	private void digWall(int w, int h, int x, int y, Maze.Piece[][] field) {
		throw new NotImplementedException();
	}
}
