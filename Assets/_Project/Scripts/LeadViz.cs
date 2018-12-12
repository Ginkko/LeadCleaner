using System;
using UnityEngine;
using UnityEngine.UI;

public class LeadViz : MonoBehaviour
{
    [SerializeField] private Text idText;
    [SerializeField] private Text emailText;
    [SerializeField] private Text firstNameText;
    [SerializeField] private Text lastNameText;
    [SerializeField] private Text addressText;
    [SerializeField] private Text dateText;

    public void Initialize(Lead lead)
    {
        idText.text = lead._id;
        emailText.text = lead.email;
        firstNameText.text = lead.firstName;
        lastNameText.text = lead.lastName;
		addressText.text = lead.address;
        dateText.text = lead.entryDate.ToShortDateString();
    }
}
