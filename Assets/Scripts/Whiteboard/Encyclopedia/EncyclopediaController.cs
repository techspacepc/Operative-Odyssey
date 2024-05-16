using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhiteboardController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pageOrganText, pageTitleText, pageInfoText, pageNumberingText;
    [SerializeField]
    private Image pageImageBox, rightArrow, leftArrow;
    [SerializeField]
    private List<OrganInfoData> organList = new();

    private OrganInfoData organData;
    private int currentOrganNumber, currentPageNumber;

    private void Start()
    {
        // TODO: add in a default explainer page before showing the organs.
        organData = organList[currentOrganNumber];
        UpdateOrganInfo();
    }

    //top button calls upon this. its rather hard coded rn, can polish it in the future.
    public void OrganSwitch(int switchToOrganNumber)
    {
        currentOrganNumber = switchToOrganNumber;
        currentPageNumber = 0;
        UpdateOrganInfo();
    }

    /// <summary>
    /// updates the current organ data to match the current organ and page.
    /// update currentOrganNumber or currentPageNumber and then call upon this to see a change.
    /// </summary>
    private void UpdateOrganInfo()
    {
        Color defaultColor = Color.white;
        int totalPages = organData.pages.Length;
        int nextPage = currentPageNumber + 1;
        int prevPage = currentPageNumber - 1;

        rightArrow.color = nextPage >= totalPages ? Color.gray : defaultColor;
        leftArrow.color = prevPage < 0 ? Color.gray : defaultColor;

        pageNumberingText.text = (nextPage + "/" + organData.pages.Length); //next page is used here as otherwise page 0 exists, it works this way I swear - Dirk

        organData = organList[currentOrganNumber];
        OrganInfoData.OrganInfoEntry currentOrganPage = organData.pages[currentPageNumber];

        pageOrganText.text = organData.OrganName;
        pageTitleText.text = currentOrganPage.PageTitle;
        pageInfoText.text = currentOrganPage.PageInfo;
        pageImageBox.sprite = currentOrganPage.PageImage;
    }

    public void PageUp()
    {
        //check to not go beyond max page of current organ.
        if ((organData.pages.Length - 1) > currentPageNumber) currentPageNumber++;
        UpdateOrganInfo();
    }

    public void PageDown()
    {
        //check to not go beyond min page of current organ.
        if (currentPageNumber > 0) currentPageNumber--;
        UpdateOrganInfo();
    }
}
