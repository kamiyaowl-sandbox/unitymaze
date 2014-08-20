using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class RandomUtil {
	private static System.Random random = new System.Random();
	public static float Value { get { return (float)random.NextDouble(); } }
	/// <summary>
	/// min~maxの間（maxを含まない）乱数を生成します
	/// </summary>
	/// <param name="min"></param>
	/// <param name="max"></param>
	/// <returns></returns>
	public static int Range(int min, int max) {
		return random.Next(max + 1 - min) + min;
	}

	public static void Call(params Action[] actions) {
		actions.TakeRandom()();
	}
	public static T Call<T>(params Func<T>[] actions) {
		return actions.TakeRandom()();
	}
	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> src) {
		return src.Select(x => new { Index = random.Next(), Value = x }).OrderBy(x => x.Index).Select(x => x.Value);
	}
	public static T TakeRandom<T>(this IEnumerable<T> src) {
		return src.ElementAt(random.Next(src.Count()));
	}
}
