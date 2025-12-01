using UnityEngine;
using UnityEngine.UI;

public class EvidenceScript : MonoBehaviour
{
    bool hasBeenFound = false;

    public Texture evImage;
    public string evName;
    public string evLocation;
    public string evDescription;

    int evNum = 0;

    EvidenceManager evManager;

    private void Start()
    {

        try
        {
            evManager = GameObject.FindGameObjectWithTag("EvManager").GetComponent<EvidenceManager>();
        }
        catch
        {
            Debug.Log("Evidence Manager Not Found");
        }

        if (evManager != null)
        {
            evManager.AddEvidence(this.gameObject);
        }

    }

    public bool Found() // called when Evidence has been found. returns if it was found before
    {
        if (hasBeenFound) return true;
        else
        {
            hasBeenFound = true;
            evNum = evManager.FoundEvidence(this.gameObject, evName, evLocation, evDescription, evImage);
        }
            return false;
    }
}
