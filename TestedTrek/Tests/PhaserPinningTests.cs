using System;
using System.Text;

using NUnit.Framework;
using StarTrek;
using Untouchables;

[TestFixture]
public class PhaserPinningTests
{
	private Game game;
	private MockGalaxy context;

	const int EnergyInNewGame = 10000;

	[SetUp]
	public void SetUp()
	{
		game = new Game(new MockRandom());
		context = new MockGalaxy();
		context.SetValueForTesting("command", "phaser");
	}

	[Test]
	public void PhasersFiredWithInsufficientEnergy()
	{
		context.SetValueForTesting("amount", (EnergyInNewGame + 1).ToString());
		game.FireWeapon(context);
		Assert.AreEqual("Insufficient energy to fire phasers! || ", context.GetAllOutput());
	}

	[Test]
	public void PhasersFiredWhenKlingonOutOfRange_AndEnergyExpendedAnyway()
	{
		int maxPhaserRange = 4000;
		int outOfRange = maxPhaserRange + 1;
		context.SetValueForTesting("amount", "1000");
		context.SetValueForTesting("target", new MockKlingon(outOfRange));

		game.FireWeapon(context);
		Assert.AreEqual("Klingon out of range of phasers at " + outOfRange + " sectors... || ", context.GetAllOutput());
		Assert.AreEqual(EnergyInNewGame - 1000, game.WeaponRemainingAmunition);
	}

	[Test]
	public void PhasersFiredKlingonDestroyed()
	{
		MockKlingon klingon = new MockKlingon(2000, 200);
		context.SetValueForTesting("amount", "1000");
		context.SetValueForTesting("target", klingon);

		game.FireWeapon(context);
		Assert.AreEqual("Phasers hit Klingon at 2000 sectors with 400 units || Klingon destroyed! || ", context.GetAllOutput());
		Assert.AreEqual(EnergyInNewGame - 1000, game.WeaponRemainingAmunition);
		Assert.IsTrue(klingon.DeleteWasCalled());
	}

	[Test]
	public void PhasersDamageOfZeroStillHits_AndNondestructivePhaserDamageDisplaysRemaining()
	{
		string minimalFired = "0";
		string minimalHit = "1";
		context.SetValueForTesting("amount", minimalFired);
		context.SetValueForTesting("target", new MockKlingon(2000, 200));

		game.FireWeapon(context);
		Assert.AreEqual("Phasers hit Klingon at 2000 sectors with " + minimalHit + " units || Klingon has 199 remaining || ", context.GetAllOutput());

		//TODO: Acknowledged bug Isn't this also a bug?  I *ask* to fire zero, and I still hit?
	}

}

