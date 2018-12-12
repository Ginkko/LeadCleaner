using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Newtonsoft.Json;
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

    public List<Lead> DeserializeJson(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<List<Lead>>(json);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deseralizing data:");
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

    [Test]
    public void DeserializeJsonTest()
    {
        List<Lead> leads = new List<Lead>();
        string json = LoadJsonFromDisk(Application.dataPath + "leads.json");
        leads = DeserializeJson(json);

        Assert.That(string.IsNullOrEmpty(json), Is.False);
    }

    #endregion

}

