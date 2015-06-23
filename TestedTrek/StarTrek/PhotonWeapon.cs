using System;

namespace StarTrek
{
	public class PhotonWeapon : IWeapon
	{
		private const string InsufficientAmunitionMessage = "No more photon torpedoes!";
		private const string TargetMissedMessage = "Torpedo missed Klingon at {0} sectors...";
		private const string TargetHitMessage = "Photons hit Klingon at {0} sectors with {1} units";
		private const string TargetHasRemainingMessage = "Klingon has {0} remaining";
		private const string TargetDestroyedMessage = "Klingon destroyed!";
		private readonly Random _random;
		private int _availableAmunitionCount;

		public PhotonWeapon(Random random)
		{
			_random = random;
		}

		public int AvailableAmunition
		{
			get
			{
				return _availableAmunitionCount;
			}
		}

		public void Fire(Writer writer, WeaponSettings weaponSettings)
		{
			int amunition = weaponSettings.Amount;
			Klingon enemy = weaponSettings.Target;
			if (amunition <= 0)
			{
				writer.WriteLine(InsufficientAmunitionMessage);
				return;
			}

			int distance = enemy.Distance();
			if ((Rnd(4) + ((distance/500) + 1) > 7))
			{
				writer.WriteLine(string.Format(TargetMissedMessage, distance));
			}
			else
			{
				int damage = 800 + Rnd(50);
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
			amunition -= 1;
			_availableAmunitionCount = amunition;
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