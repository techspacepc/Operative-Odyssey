using Tags;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class TourHandler : MonoBehaviour
{
    [SerializeField] private GameObject museumParent;
    [SerializeField] private GameObject[] tourParents;

    private XRSimpleInteractable[] tourExitButtons;

    private Transform playerTransform;

    private Vector3 resetPosition = new(1, 0, 1);

    private int currentTour;

    private void StartTour()
    {
        DelegatePortalCollision.OnPortalEntered -= StartTour;

        museumParent.SetActive(false);
        tourParents[currentTour].SetActive(true);

        DelegatePortalCollision.OnPortalEntered += ContinueTour;
    }

    private void ContinueTour()
    {
        tourParents[currentTour++].SetActive(false);
        tourParents[currentTour].SetActive(true);
    }

    private void ExitTour(SelectEnterEventArgs _)
    {
        DelegatePortalCollision.OnPortalEntered -= ContinueTour;

        foreach (GameObject tour in tourParents)
            tour.SetActive(false);

        museumParent.SetActive(true);

        playerTransform.position = resetPosition;

        currentTour = default;

        DelegatePortalCollision.OnPortalEntered += StartTour;
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<Transform>();

        museumParent.GetComponentInChildren<SphereCollider>().AddComponent<DelegatePortalCollision>();

        tourExitButtons = new XRSimpleInteractable[tourParents.Length];

        for (int i = 0; i < tourParents.Length; i++)
        {
            GameObject tour = tourParents[i];

            tourExitButtons[i] = tour.GetComponentInChildren<XRPokeFilter>().GetComponent<XRSimpleInteractable>();

            if (i == tourParents.Length - 1) return;

            tour.GetComponentInChildren<SphereCollider>().AddComponent<DelegatePortalCollision>();
        }
    }

    private void OnEnable()
    {
        DelegatePortalCollision.OnPortalEntered += StartTour;

        foreach (XRSimpleInteractable interactable in tourExitButtons)
            interactable.selectEntered.AddListener(ExitTour);
    }

    private void OnDisable()
    {
        DelegatePortalCollision.OnPortalEntered -= StartTour;
        DelegatePortalCollision.OnPortalEntered -= ContinueTour;

        foreach (XRSimpleInteractable interactable in tourExitButtons)
            interactable.selectEntered.RemoveListener(ExitTour);
    }
}