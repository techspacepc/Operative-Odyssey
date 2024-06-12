using UnityEngine;

namespace Organs
{
    public class OrganIdentifier : MonoBehaviour, IOrgan
    {
        public Renderer FullOrganRenderer { get; set; }
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
        Renderer FullOrganRenderer { get; set; }
        bool IsGrabbed { get; set; }
    }
}