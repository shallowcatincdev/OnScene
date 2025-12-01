using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceManager : MonoBehaviour
{
    public List<GameObject> evidenceList = new List<GameObject>();
    public List<GameObject> evidenceFound = new List<GameObject>();

    UIManager uiManager;

    private void Start()
    {
        try
        {
            uiManager = GameObject.FindGameObjectWithTag("Player").GetComponent<UIManager>();
        }
        catch
        {
            Debug.Log("Could not find UIManager");
        }
    }


    public void AddEvidence(GameObject item) // called on start to initialize evidence
    {
        evidenceList.Add(item);
    }

    public int FoundEvidence(GameObject item, string evName, string evLocation, string evDiscription, Texture evImage) // called by evidence when it is found
    {
        evidenceFound.Add(item);
        evidenceList.Remove(item);

        uiManager.AddEvidenceInfo(evidenceFound.Count, evName, evLocation, evDiscription, evImage);

        return  evidenceFound.Count;
    }

}
