using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public List<GameObject> evidenceList = new List<GameObject>();
    public List<GameObject> evidenceFound = new List<GameObject>();

    public void AddEvidence(GameObject item) // called on start to initialize evidence
    {
        evidenceList.Add(item);
    }

    public void FoundEvidence(GameObject item) // called by evidence when it is found
    {
        evidenceFound.Add(item);
        evidenceList.Remove(item);
    }
}
