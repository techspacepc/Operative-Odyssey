using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhiteboardController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pageOrganText, pageTitleText, pageInfoText;
    [SerializeField]
    private Image pageImageBox;
    [SerializeField]
    private List<OrganInfoData> organList = new();

    private OrganInfoData organData;
    private int currentOrganNumber, currentPageNumber;

    private void Start()
    {
        //todo: add in a default explainer page before showing the organs.
        organData = organList[currentOrganNumber];
        OrganInfoData.OrganInfoEntry currentOrganPage = organData.pages[currentPageNumber];

        pageOrganText.text = organData.OrganName;
        pageTitleText.text = currentOrganPage.PageTitle;
        pageInfoText.text = currentOrganPage.PageInfo;
        pageImageBox.sprite = currentOrganPage.PageImage;
    }

    //top button calls upon this. its rather hard coded rn, can polish it in the future.
    public void organSwitch(int switchToOrganNumber)
    {
        currentOrganNumber = switchToOrganNumber;
        currentPageNumber = 0;
        updateOrganInfo();
    }

    /// <summary>
    /// updates the current organ data to match the current organ and page.
    /// update currentOrganNumber or currentPageNumber and then call upon this to see a change.
    /// </summary>
    private void updateOrganInfo()
    {
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
        updateOrganInfo();
    }

    public void PageDown()
    {
        //check to not go beyond min page of current organ.
        if (currentPageNumber > 0) currentPageNumber--;
        updateOrganInfo();
    }
}
