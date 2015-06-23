namespace StarTrek
{
	public abstract class TemplateWeapon : IWeapon
	{
		protected Writer Writer;

		public void Fire(Writer writer, WeaponSettings weaponSettings)
		{
			Writer = writer;
			var amount = weaponSettings.Amount;
			var enemy = weaponSettings.Target;

			if (AvailableAmunition < amount)
			{
				InsufficientAmunition();
				return;
			}

			var distance = enemy.Distance();

			if (HasHitEnemy(distance))
			{
				HitEnemy(amount, distance, enemy);
			}
			else
			{
				OutOfDistance(distance);
			}

			DecreaseAmunition(amount);
		}

		public int AvailableAmunition { get; set; }

		protected abstract void HitEnemy(int amount, int distance, Klingon enemy);

		protected abstract void OutOfDistance(int distance);

		protected abstract bool HasHitEnemy(int distance);

		protected abstract void DecreaseAmunition(int amount);

		protected abstract void InsufficientAmunition();
	}
}