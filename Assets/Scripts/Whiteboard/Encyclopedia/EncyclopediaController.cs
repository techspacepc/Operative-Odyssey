using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhiteboardController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pageOrganText, pageTitleText, pageInfoText, pageNumberingText;
    [SerializeField]
    private Image pageImageBox;
    [SerializeField]
    private Button rightArrowButton, leftArrowButton;
    [SerializeField]
    private Transform organBar;
    [SerializeField]
    private List<OrganInfoData> organList = new();
    private Button[] organSwitches;

    private OrganInfoData organData;
    private int currentOrganNumber, currentPageNumber;

    private void Awake()
    {
        organSwitches = organBar.GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        // TODO: add in a default explainer page before showing the organs.
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
        organData = organList[currentOrganNumber];
        OrganInfoData.OrganInfoEntry currentOrganPage = organData.pages[currentPageNumber];

        int totalPages = organData.pages.Length;
        int nextPage = currentPageNumber + 1;
        int prevPage = currentPageNumber - 1;

        rightArrowButton.interactable = nextPage < totalPages;
        leftArrowButton.interactable = prevPage >= 0;

        pageNumberingText.text = $"{nextPage} / {organData.pages.Length}"; //next page is used here as otherwise page 0 exists, it works this way I swear - Dirk

        pageOrganText.text = organData.OrganName;
        pageTitleText.text = currentOrganPage.PageTitle;
        pageInfoText.text = currentOrganPage.PageInfo;
        pageImageBox.sprite = currentOrganPage.PageImage;
    }

    public void PageUp()
    {
        currentPageNumber++;
        UpdateOrganInfo();
    }

    public void PageDown()
    {
        currentPageNumber--;
        UpdateOrganInfo();
    }

    private void OnEnable()
    {
        leftArrowButton.onClick.AddListener(PageDown);
        rightArrowButton.onClick.AddListener(PageUp);

        for (int i = 0; i < organSwitches.Length; i++)
        {
            int j = i; // To prevent lambda capturing https://unity.huh.how/runtime-exceptions/indexoutofrangeexception
            Button button = organSwitches[i];
            button.onClick.AddListener(() => OrganSwitch(j));
        }
    }

    private void OnDisable()
    {
        leftArrowButton.onClick.RemoveListener(PageDown);
        rightArrowButton.onClick.RemoveListener(PageUp);

        foreach (Button button in organSwitches)
            button.onClick.RemoveAllListeners();
    }
}