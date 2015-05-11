using System;
using NUnit.Framework;

[TestFixture]
public class TestIDEIntegration {
    [Test]
    public void FirstExample() {
        Assert.AreEqual("happy?", "HAPPY?".ToLower());
    }
}
