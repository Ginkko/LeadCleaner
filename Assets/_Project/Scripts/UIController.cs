using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private string loadFileDefault = "leads.json";
    [SerializeField]
    private string writeFileDefault = "cleanedLeads.json";

    [Header("Ref")]
    [SerializeField]
    private InputField LoadPath;
    [SerializeField]
    private InputField WritePath;
    [SerializeField]
    private Button cleanButton;
    [SerializeField]
    private LeadCleaner leadCleaner;
	[SerializeField]
	private GameObject leadVizPrefab;
	[SerializeField]
	private Transform leadRegion;

    private void Awake()
    {
		cleanButton.onClick.AddListener(OnCleanButton);
    }
    private void Start()
    {
        LoadPath.text = Application.streamingAssetsPath + "\\" + loadFileDefault;
        WritePath.text = Application.streamingAssetsPath + "\\" + writeFileDefault;
    }

    private void OnCleanButton()
    {
		List<Lead> leads = leadCleaner.CleanLeads(LoadPath.text, WritePath.text);
		foreach (Lead lead in leads)
		{
			GameObject go = Instantiate(leadVizPrefab);
			go.transform.SetParent(leadRegion);
			go.transform.localScale = Vector3.one;
			LeadViz leadViz = go.GetComponent<LeadViz>();
			leadViz.Initialize(lead);
		}
    }
}
