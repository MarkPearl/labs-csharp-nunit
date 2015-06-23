using System;

namespace StarTrek
{
	public class WeaponFactory
	{
		private readonly Random _random;

		public WeaponFactory(Random random)
		{
			_random = random;
		}

		public IWeapon Create(string name)
		{
			if (name.Equals("phaser"))
			{
				return new PhaserWeapon(_random);
			}

			if (name.Equals("photon"))
			{
				return new PhotonWeapon(_random);
			}

			//TODO: throw an exception maybe?
			return null;
		}
		
	}
}