using System;

namespace StarTrek
{
	public class PhaserWeapon : IWeapon
	{
		private const string InsufficientAmunitionMessage = "Insufficient energy to fire phasers!";
		private const string TargetMissedMessage = "Klingon out of range of phasers at {0} sectors...";
		private const string TargetDestroyedMessage = "Klingon destroyed!";
		private const string TargetHasRemainingMessage = "Klingon has {0} remaining";
		private const string TargetHitMessage = "Phasers hit Klingon at {0} sectors with {1} units";

		private readonly Random _random;
		private int _availableEnergy;

		public PhaserWeapon(Random random)
		{
			_random = random;
			_availableEnergy = 10000;
		}

		public int AvailableAmunition
		{
			get { return _availableEnergy; }
		}

		public void Fire(Writer writer, WeaponSettings weaponSettings)
		{
			int amount = weaponSettings.Amount;
			Klingon enemy = weaponSettings.Target;
			if (_availableEnergy < amount)
			{
				writer.WriteLine(InsufficientAmunitionMessage);
				return;
			}

			int distance = enemy.Distance();
			if (distance > 4000)
			{
				writer.WriteLine(string.Format(TargetMissedMessage, distance));
			}
			else
			{
				int damage = amount - (((amount/20)*distance/200) + Rnd(200));
				if (damage < 1)
				{
					damage = 1;
				}

				writer.WriteLine(string.Format(TargetHitMessage, distance, damage));
				if (damage < enemy.GetEnergy())
				{
					enemy.SetEnergy(enemy.GetEnergy() - damage);
					writer.WriteLine(string.Format(TargetHasRemainingMessage, enemy.GetEnergy()));
				}
				else
				{
					writer.WriteLine(TargetDestroyedMessage);
					enemy.Delete();
				}
			}
			_availableEnergy -= amount;
		}

		// note we made generator public in order to mock it
		// it's ugly, but it's telling us something about our *design!* ;-)
		//public static Random generator = new Random();
		private int Rnd(int maximum)
		{
			return _random.Next(maximum);
		}
	}
}