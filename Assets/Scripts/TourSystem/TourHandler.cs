using Tags;
using Unity.VisualScripting;
using UnityEngine;

public class TourHandler : MonoBehaviour
{
    [SerializeField] private GameObject museumParent;
    [SerializeField] private GameObject museumLightingParent;
    [SerializeField] private GameObject tourLightingParent;
    private CharacterController playerController;

    private Vector3 resetPosition = new(1, 0, 1);
    private Vector3 enterTourPosition = new(26.1000004f,0f,-37.2999992f);

    private void StartTour()
    {
        DelegatePortalCollision.OnPortalEntered -= StartTour;
        DelegatePortalCollision.OnPortalEntered += ExitTour;

        //enable/disable parents
        tourLightingParent.SetActive(true);
        museumLightingParent.SetActive(false);

        //teleport player
        playerController.enabled = false;
        playerController.transform.localPosition = enterTourPosition;
        playerController.enabled = true;
    }

    private void ExitTour()
    {
        DelegatePortalCollision.OnPortalEntered -= ExitTour;
        DelegatePortalCollision.OnPortalEntered += StartTour;

        //enable/disable parents
        museumLightingParent.SetActive(true);
        tourLightingParent.SetActive(false);

        //teleport player
        playerController.enabled = false;
        playerController.transform.position = resetPosition;
        playerController.enabled = true;
    }

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<CharacterController>();

        museumParent.GetComponentInChildren<SphereCollider>().AddComponent<DelegatePortalCollision>();
    }

    private void OnEnable()
    {
        DelegatePortalCollision.OnPortalEntered += StartTour;
    }

    private void OnDisable()
    {
        DelegatePortalCollision.OnPortalEntered -= StartTour;
    }
}