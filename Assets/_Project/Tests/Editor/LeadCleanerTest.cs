using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LeadCleanerTest {

	[Test]
	public void BasicTest()
	{
		bool testBool = false;
		Assert.AreEqual(false, testBool);
	}

	[Test]
	public void LoadJsonFromDiskTest()
	{
		bool testBool = false;
		Assert.AreEqual(false, testBool);
	}

}
