using UnityEngine;

[CreateAssetMenu(fileName = nameof(OrganInfoData), menuName = "Custom/" + nameof(OrganInfoData), order = 1)]
public class OrganInfoData : ScriptableObject
{
    [System.Serializable]
    public struct OrganInfoEntry
    {
        public string PageTitle;
        public string PageInfo;
        public Sprite PageImage;
    }

    public string OrganName;
    public OrganInfoEntry[] pages;
}
