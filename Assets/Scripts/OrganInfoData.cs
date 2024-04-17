using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "OrganInfoData", menuName = "Resources/OrganInfoData", order = 1)]
public class OrganInfoData : ScriptableObject
{
    [System.Serializable]

    public struct OrganInfoEntry
    {
        public string PageTitle;
        public string PageInfo;
        public Image PageImage;
    }

    public string OrganName;
    public OrganInfoEntry[] pages;
}
