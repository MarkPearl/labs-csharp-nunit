using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Untouchables;

[TestFixture]
public class PhotonPinningTests {
    private Game game;
    private MockGalaxy context;

    [TearDown]
    public void RemoveTheMockRandomGeneratorForOtherTests_IReallyWantToRefactorThatStaticVariableSoon() {
        Game.generator = new Random();
    }

    [SetUp]
    public void SetUp() {
        game = new Game();
        context = new MockGalaxy();
        context.SetValueForTesting("command", "photon");
    }

    [Test]
    public void NotifiedIfNoTorpedoesRemain() {
        game.Torpedoes = 0;
        context.SetValueForTesting("target", new MockKlingon(2000, 200));
        game.FireWeapon(context);
        Assert.AreEqual("No more photon torpedoes! || ",
            context.GetAllOutput());
    }

    [Test]
    public void TorpedoMissesDueToRandomFactors() {
        int distanceWhereRandomFactorsHoldSway = 2500;
        context.SetValueForTesting("target", new MockKlingon(distanceWhereRandomFactorsHoldSway, 200));
        Game.generator = new MockRandom(); // without this the test would often fail
        game.FireWeapon(context);
        Assert.AreEqual("Torpedo missed Klingon at 2500 sectors... || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
    }

    [Test]
    public void TorpedoMissesDueToDistanceAndCleverKlingonEvasiveActions() {
        int distanceWhereTorpedoesAlwaysMiss = 3500;
        context.SetValueForTesting("target", new MockKlingon(distanceWhereTorpedoesAlwaysMiss, 200));
        game.FireWeapon(context);
        Assert.AreEqual("Torpedo missed Klingon at 3500 sectors... || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
    }

    [Test]
    public void TorpedoDestroysKlingon() {
        MockKlingon klingon = new MockKlingon(500, 200);
        context.SetValueForTesting("target", klingon);
        Game.generator = new MockRandom();
        game.FireWeapon(context);
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon destroyed! || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
        Assert.IsTrue(klingon.DeleteWasCalled());

    }

    [Test]
    public void TorpedoDamagesKlingon() {
        context.SetValueForTesting("target", new MockKlingon(500, 2000));
        Game.generator = new MockRandom();
        game.FireWeapon(context);
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon has 1175 remaining || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
    }

}
