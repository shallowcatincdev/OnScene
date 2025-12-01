using UnityEngine;

public class MarkerTool : MonoBehaviour
{
    bool toolEnabled = true;
    public Camera cam;
    public float maxDist = 10;
    public void OnClick()
    {
        if (toolEnabled)
        {
            RaycastHit hit;
                
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDist))
            {
                if (hit.transform.CompareTag("Evidence"))
                {
                    if (hit.transform.gameObject.GetComponent<EvidenceScript>().Found())
                    { // already found
                        Debug.Log("Evidence Already Found");
                    }
                    else
                    { // Not already found
                        Debug.Log("New Evidence Has Been Found");
                    }
                }
            }
            
        }
    }
}
