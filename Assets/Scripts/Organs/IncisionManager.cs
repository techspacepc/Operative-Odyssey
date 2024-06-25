using Organs;
using Pathing;
using System;
using Tags;
using UnityEngine;

public class IncisionManager : MonoBehaviour
{
    [SerializeField] private GameObject cutOrgan;

    private OutOfBoundsChecker organBoundsChecker, scalpelBoundsChecker;
    private IOrgan iOrgan;

    public Action<int> onIncisionMade;
    public event Action ResetIncisions;
    public static Material uncutMat, cutMat;
    private bool[] incisions;

    private void ClearAllIncisions()
    {
        for (int i = 0; i < incisions.Length; i++)
            incisions[i] = false;

        ResetIncisions();
    }

    private void OnIncisionMade(int incisionIndex)
    {
        incisions[incisionIndex] = true;

        foreach (bool incision in incisions)
            if (!incision) return;

        Cut();
    }

    [ContextMenu(nameof(Cut))]
    private void Cut()
    {
        if (!iOrgan.FullOrganRenderer.enabled) return;

        GameObject organ = Instantiate(cutOrgan, transform.position, Quaternion.identity, transform.parent);

        foreach (Transform child in organ.transform)
        {
            OrganIdentifier identifier = child.gameObject.AddComponent<OrganIdentifier>();
            identifier.FullOrganRenderer = iOrgan.FullOrganRenderer;
            identifier.Organ = iOrgan.Organ;
        }

        organBoundsChecker.RecallThis(gameObject);
        scalpelBoundsChecker.recall();
        iOrgan.FullOrganRenderer.enabled = false;

        ClearAllIncisions();
    }

    private void Awake()
    {
        iOrgan = GetComponent<IOrgan>();

        incisions = new bool[GetComponentInChildren<EdgeCollider2D>().transform.childCount];

        uncutMat = Resources.Load<Material>(Path.UncutMaterial);
        cutMat = Resources.Load<Material>(Path.Cutmaterial);
    }

    private void Start()
    {
        scalpelBoundsChecker = GameObject.FindGameObjectWithTag(Tag.Scalpel).GetComponent<OutOfBoundsChecker>();

        organBoundsChecker = GetComponent<OutOfBoundsChecker>();
        iOrgan.BoundsChecker = organBoundsChecker;
    }

    private void OnEnable() => onIncisionMade += OnIncisionMade;

    private void OnDisable() => onIncisionMade -= OnIncisionMade;
}
