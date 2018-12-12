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

public class LeadContainer
{
    public List<Lead> Leads;
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
            Debug.Log("Error reading file:");
            Debug.Log(e.Message);
        }

        return null;
    }

    public LeadContainer DeserializeJson(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<LeadContainer>(json);
        }
        catch (Exception e)
        {
            Debug.Log("Error deseralizing data:");
            Debug.Log(e.Message);
        }

        return null;
    }

    #region Tests

    [Test]
    public void LoadJsonFromDiskTest()
    {
        string json = LoadJsonFromDisk(Application.dataPath + "\\leads.json");

        Assert.That(string.IsNullOrEmpty(json), Is.False);
    }

    [Test]
    public void DeserializeJsonTest()
    {
        string json = LoadJsonFromDisk(Application.dataPath + "\\leads.json");
        LeadContainer leads = DeserializeJson(json);

        Assert.That(leads, !Is.Null);
    }

    #endregion

}

