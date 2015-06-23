using NUnit.Framework;
using StarTrek;

[TestFixture]
public class PhotonPinningTests {
    private Game game;
    private MockGalaxy context;

    [SetUp]
    public void SetUp()
    {
	    game = new Game(new MockRandom());
        context = new MockGalaxy();
        context.SetValueForTesting("command", "photon");
    }

    [Test]
    public void NotifiedIfNoTorpedoesRemain() {
        context.SetValueForTesting("amount", "0");
        context.SetValueForTesting("target", new MockKlingon(2000, 200));
        game.FireWeapon(context);
        Assert.AreEqual("No more photon torpedoes! || ", context.GetAllOutput());
    }

    [Test]
    public void TorpedoMissesDueToRandomFactors() {
        int distanceWhereRandomFactorsHoldSway = 2500;
        context.SetValueForTesting("target", new MockKlingon(distanceWhereRandomFactorsHoldSway, 200));
		context.SetValueForTesting("amount", "8");
        game.FireWeapon(context);
        Assert.AreEqual("Torpedo missed Klingon at 2500 sectors... || ", context.GetAllOutput());
        Assert.AreEqual(7, game.WeaponRemainingAmunition);
    }

    [Test]
    public void TorpedoMissesDueToDistanceAndCleverKlingonEvasiveActions() {
        int distanceWhereTorpedoesAlwaysMiss = 3500;
        context.SetValueForTesting("target", new MockKlingon(distanceWhereTorpedoesAlwaysMiss, 200));
		context.SetValueForTesting("amount", "8");
        game.FireWeapon(context);
        Assert.AreEqual("Torpedo missed Klingon at 3500 sectors... || ", context.GetAllOutput());
        Assert.AreEqual(7, game.WeaponRemainingAmunition);
    }

    [Test]
    public void TorpedoDestroysKlingon() {
        MockKlingon klingon = new MockKlingon(500, 200);
        context.SetValueForTesting("target", klingon);
		context.SetValueForTesting("amount", "8");
        game.FireWeapon(context);
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon destroyed! || ", context.GetAllOutput());
        Assert.AreEqual(7, game.WeaponRemainingAmunition);
        Assert.IsTrue(klingon.DeleteWasCalled());

    }

    [Test]
    public void TorpedoDamagesKlingon() {
        context.SetValueForTesting("target", new MockKlingon(500, 2000));
		context.SetValueForTesting("amount", "8");
        game.FireWeapon(context);
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon has 1175 remaining || ", context.GetAllOutput());
        Assert.AreEqual(7, game.WeaponRemainingAmunition);
    }

}
