using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Newtonsoft.Json;
using UnityEngine;

public class Lead
{
    public string _id;
    public string email;
    public string firstName;
    public string lastName;
    public string address;
    public DateTime entryDate;
}

public class LeadContainer
{
    public List<Lead> leads;
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

    private LeadContainer DeserializeJson(string json)
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

    private LeadContainer CleanLeadsById(LeadContainer leadContainer)
    {
        return leadContainer;
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
        LeadContainer leadContainer = DeserializeJson(json);

        Assert.That(leadContainer.leads[0], !Is.Null);
    }

    [Test]
    public void CleanLeadsByIdTest()
    {
        string json = LoadJsonFromDisk(Application.dataPath + "\\leads.json");
        LeadContainer leadContainer = DeserializeJson(json);

        leadContainer = CleanLeadsById(leadContainer);

        bool duplicateExists = false;

        for (int i = 0; i < leadContainer.leads.Count; i++)
        {
            for (int j = i + 1; j < leadContainer.leads.Count; j++)
            {
                if (leadContainer.leads[i]._id == leadContainer.leads[j]._id)
                {
                    Debug.LogFormat("Duplicate found [{0}]: {1}, [{2}]: {3}", i, leadContainer.leads[i]._id, j, leadContainer.leads[j]._id);
                    duplicateExists = true;
                }
            }
        }

        Assert.That(duplicateExists, Is.False);
    }

    #endregion

}

