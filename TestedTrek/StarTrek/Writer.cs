namespace StarTrek
{
	public class Writer
	{
		private readonly Galaxy _galaxy;

		public Writer(Galaxy galaxy)
		{
			_galaxy = galaxy;
		}

		public void WriteLine(string value)
		{
			_galaxy.WriteLine(value);
		} 
	}
}