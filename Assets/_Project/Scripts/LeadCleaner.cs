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

    private List<Lead> CleanLeads(LeadContainer leadContainer)
    {
        List<Lead> cleanedLeads = new List<Lead>();

        foreach (Lead newLead in leadContainer.leads)
        {
            AddIfNotDuplicate(newLead, cleanedLeads);
        }

        return cleanedLeads;
    }

    private void AddIfNotDuplicate(Lead newLead, List<Lead> existingLeads)
    {
        foreach (Lead existingLead in existingLeads)
        {
            if (existingLead._id == newLead._id || existingLead.email == newLead.email)
            {
                Debug.LogFormat("Duplicate entry found");
                if (newLead.entryDate > existingLead.entryDate)
                {
                    Debug.LogFormat("New lead is more recent, updating existing with new data. Lead: {0} => {1}", LeadAsString(existingLead), LeadAsString(newLead));
                    existingLead._id = newLead._id;
                    existingLead.email = newLead.email;
                    existingLead.firstName = newLead.firstName;
                    existingLead.lastName = newLead.lastName;
                    existingLead.entryDate = newLead.entryDate;
                    return;
                }
                else
                {
                    Debug.LogFormat("Existing lead is more recent, removing lead from data set: {0}", LeadAsString(newLead));
                    return;
                }
            }
        }

        existingLeads.Add(newLead);
    }

    private string LeadAsString(Lead lead)
    {
        return string.Format("Id: {0}, Email: {1}, FirstName: {2}, LastName: {3}, EntryDate: {4}", lead._id, lead.email, lead.firstName, lead.lastName, lead.entryDate);
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

        List<Lead> leads = CleanLeads(leadContainer);

        bool duplicateExists = false;

        for (int i = 0; i < leads.Count; i++)
        {
            for (int j = i + 1; j < leads.Count; j++)
            {
                if (leads[i]._id == leads[j]._id)
                {
                    Debug.LogFormat("Duplicate found [{0}]: {1}, [{2}]: {3}", i, leads[i]._id, j, leads[j]._id);
                    duplicateExists = true;
                }
            }
        }

        Assert.That(duplicateExists, Is.False);
    }

    [Test]
    public void CleanLeadsByEmailTest()
    {
        string json = LoadJsonFromDisk(Application.dataPath + "\\leads.json");
        LeadContainer leadContainer = DeserializeJson(json);

        List<Lead> leads = CleanLeads(leadContainer);

        bool duplicateExists = false;

        for (int i = 0; i < leads.Count; i++)
        {
            for (int j = i + 1; j < leads.Count; j++)
            {
                if (leads[i].email == leads[j].email)
                {
                    Debug.LogFormat("Duplicate found [{0}]: {1}, [{2}]: {3}", i, leads[i].email, j, leads[j].email);
                    duplicateExists = true;
                }
            }
        }

        Assert.That(duplicateExists, Is.False);
    }

    #endregion
}

