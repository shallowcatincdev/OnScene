using UnityEngine;

public class EvidenceScript : MonoBehaviour
{
    bool hasBeenFound = false;
    public bool Found() // called when Evidence has been found. returns if it was found before
    {
        if (hasBeenFound) return true;
        else
        {
            hasBeenFound = true;
            // begin other stuff
        }
            return false;
    }
}
