using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class Lead
{
    public string Id;
    public string Email;
    public string FirstName;
    public string LastName;
    public string Address;
    public DateTime EntryDate;
}

public class LeadCleaner : MonoBehaviour
{

    public List<Lead> DeDuplicateLeads(string jsonPath)
    {
        List<Lead> leads = new List<Lead>();


        return leads;
    }

    private string LoadJsonFromDisk(string path)
    {
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                return json;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error reading file:");
            Console.WriteLine(e.Message);
        }

        return null;
    }

    #region Tests

    [Test]
    public void LoadJsonFromDiskTest()
    {
        string json = LoadJsonFromDisk(Application.dataPath + "leads.json");
        Assert.That(string.IsNullOrEmpty(json), Is.False);
    }

    #endregion

}

