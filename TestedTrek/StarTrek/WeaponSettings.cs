namespace StarTrek
{
	public class WeaponSettings
	{
		public Klingon Target { get; private set; }
		public int Amount { get; private set; }

		private WeaponSettings(Klingon target, int amount)
		{
			Target = target;
			Amount = amount;
		}

		public static WeaponSettings CreateFromGalaxy(Galaxy wg)
		{
			var weaponSettings = new WeaponSettings(
				(Klingon)wg.Variable("target"), 
				int.Parse(wg.Parameter("amount")));

			return weaponSettings;
		}
	}
}