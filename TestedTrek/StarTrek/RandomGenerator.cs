using System;

public class RandomGenerator : IRandomGenerator
{
	public static Random generator = new Random();

	public int Rnd(int maximum)
	{
		return generator.Next(maximum);
	}
}