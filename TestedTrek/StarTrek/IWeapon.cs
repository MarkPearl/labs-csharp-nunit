namespace StarTrek
{
	public interface IWeapon
	{
		int AvailableAmunition { get; }
		void Fire(Writer writer, WeaponSettings weaponSettings);
	}
}