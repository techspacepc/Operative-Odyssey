using UnityEngine;

namespace Organs
{
    public class OrganIdentifier : MonoBehaviour, IOrgan
    {
        [field: SerializeField] public OrganType Organ { get; set; }
        public bool IsGrabbed { get; set; }
    }

    public enum OrganType
    {
        Heart,
        Kidney,
        Eye
    }

    public interface IOrgan
    {
        OrganType Organ { get; set; }
        bool IsGrabbed { get; set; }
    }
}