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

    // Start is called before the first frame update
    private void Start()
    {
        organData = organList[currentOrganNumber];
        OrganInfoData.OrganInfoEntry currentOrganPage = organData.pages[currentPageNumber];

        pageOrganText.text = organData.OrganName;
        pageTitleText.text = currentOrganPage.PageTitle;
        pageInfoText.text = currentOrganPage.PageInfo;
        pageImageBox.sprite = currentOrganPage.PageImage;
    }

    public void organSwitch(int switchToOrganNumber)
    {
        currentOrganNumber = switchToOrganNumber;
        currentPageNumber = 0;
        updateOrganInfo();
    }

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
        if ((organData.pages.Length - 1) > currentPageNumber) currentPageNumber++;
        updateOrganInfo();
    }

    public void PageDown()
    {
        if (currentPageNumber > 0) currentPageNumber--;
        updateOrganInfo();
    }
}
