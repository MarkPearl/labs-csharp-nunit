using System;
using Untouchables;

namespace StarTrek
{
	public class Game
	{
		private readonly WeaponFactory _weaponFactory;
		private IWeapon _currentWeapon;

		public Game() : this(new Random())
		{
		}

		public Game(Random random)
		{
			_weaponFactory = new WeaponFactory(random);
		}

		public int WeaponRemainingAmunition
		{
			get { return _currentWeapon.AvailableAmunition; }
		}

		public void FireWeapon(WebGadget wg)
		{
			FireWeapon(new Galaxy(wg));
		}

		public void FireWeapon(Galaxy wg)
		{
			var weaponName = wg.Parameter("command");
			var writer = new Writer(wg);
			var weaponSettings = WeaponSettings.CreateFromGalaxy(wg);

			_currentWeapon = _weaponFactory.Create(weaponName);
			_currentWeapon.Fire(writer, weaponSettings);
		}
	}
}