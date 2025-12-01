using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject book;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject evPagePrefab;
    [SerializeField] GameObject mainPagePrefab;

    bool bookOpen = false;
    int currentPage = 0;
    int totalPages = 0;
    int totalEvidenceAdded = 0;
    bool nextDisabled = true;
    bool backDisabled = true;
    GameObject[] pages = new GameObject[100];

    Movement movement;

    private void Start()
    {
        try
        {
            movement = GetComponent<Movement>();
        }
        catch
        {
            Debug.Log("Error Movement Script Not Found");
        }

        pages[0] = Instantiate(mainPagePrefab, book.transform.Find("Pages"));

    }


    public void AddEvidenceInfo(int evNum, string evName, string evLocation, string evDiscription, Texture evImage)
    {
        GameObject bookLocationRef = null;

        if (totalEvidenceAdded % 4 == 0 && totalPages < 100) // check where on page evidence needs to be added. if full create a new page first.
        {
            totalPages ++;
            pages[totalPages] = Instantiate(evPagePrefab, book.transform.Find("Pages")); // create new evidence page
            pages[totalPages].SetActive(false);
            if (nextDisabled)
            {
                book.transform.Find("NextPage").gameObject.SetActive(true);
                nextDisabled = false;
            }

            bookLocationRef = pages[totalPages].transform.Find("EvidenceTL").gameObject;
            
        }
        else if(totalEvidenceAdded % 4 == 1)
        {
            bookLocationRef = pages[totalPages].transform.Find("EvidenceBL").gameObject;
        }
        else if (totalEvidenceAdded % 4 == 2)
        {
            bookLocationRef = pages[totalPages].transform.Find("EvidenceTR").gameObject;
        }
        else if (totalEvidenceAdded % 4 == 3)
        {
            bookLocationRef = pages[totalPages].transform.Find("EvidenceBR").gameObject;
        }

        if (bookLocationRef != null) // add data to page
        {
            bookLocationRef.transform.Find("EvImage").gameObject.GetComponent<RawImage>().texture = evImage;
            bookLocationRef.transform.Find("EvName").gameObject.GetComponent<TextMeshProUGUI>().text = evName;
            bookLocationRef.transform.Find("EvNumber").gameObject.GetComponent<TextMeshProUGUI>().text = "#" + evNum.ToString();
            bookLocationRef.transform.Find("EvLocation").gameObject.GetComponent<TextMeshProUGUI>().text = evLocation;
            bookLocationRef.transform.Find("EvInfo").gameObject.GetComponent<TextMeshProUGUI>().text = evDiscription;
        }
        totalEvidenceAdded++;
    }


    public void OnBook() // open/close book on button press
    {
        if (bookOpen)
        {
            bookOpen = false;
            book.SetActive(false);
            hud.SetActive(true);
            if (movement != null)
            {
                movement.DisableUiMode();
            }
        }
        else
        {
            bookOpen = true;
            book.SetActive(true);
            hud.SetActive(false);
            if (movement != null)
            {
                movement.EnableUiMode();
            }
        }
    }

    public void NextPage()
    {
        if (currentPage  < totalPages)
        {
            pages[currentPage + 1].SetActive(true);
            pages[currentPage].SetActive(false);
            currentPage ++;
            if (currentPage >= totalPages)
            {
                
                book.transform.Find("NextPage").gameObject.SetActive(false);
                nextDisabled = true;
            }
            if (backDisabled)
            {
                backDisabled = false;
                book.transform.Find("PriorPage").gameObject.SetActive(true);
            }
        }
    }

    public void BackPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage - 1].SetActive(true);
            pages[currentPage].SetActive(false);
            currentPage--;
            if (currentPage <= 0)
            {
                backDisabled = true;
                book.transform.Find("PriorPage").gameObject.SetActive(false);
            }
            if (nextDisabled)
            {
                nextDisabled = false;
                book.transform.Find("NextPage").gameObject.SetActive(true);
            }
        }
    }
}
