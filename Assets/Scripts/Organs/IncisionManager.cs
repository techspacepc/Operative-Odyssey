using MessageSuppression;
using Pathing;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class IncisionManager : MonoBehaviour
{
    [SerializeField] private GameObject cutOrgan;

    public Action<int> onIncisionMade;
    public event Action OnIncisionFailed;
    public static event Action<GameObject> OnCut;
    public static Material uncutMat, cutMat;
    private bool[] incisions;

    private readonly int lastIncisionIndex;

    [SuppressMessage(Suppress.CodeQuality.Category, Suppress.CodeQuality.CheckId, Justification = Suppress.CodeQuality.Justification)]
    private bool ClearAllIncisions(int currentIncision)
    {
        if (Mathf.Abs(lastIncisionIndex - currentIncision) > 1)
        {
            for (int i = 0; i < incisions.Length; i++)
                incisions[i] = false;

            OnIncisionFailed();

            return true;
        }
        else return false;
    }

    private void OnIncisionMade(int incisionIndex)
    {
        // Conditional cutting will be re-implemented later.
        //if (ClearAllIncisions(incisionIndex)) return; 

        incisions[incisionIndex] = true;
        //lastIncisionIndex = incisionIndex;

        foreach (bool incision in incisions)
            if (!incision) return;
        Cut();
        OnCut(gameObject);
    }

    private void Cut()
    {
        Instantiate(cutOrgan, transform.position, Quaternion.identity, transform.parent);

        Destroy(gameObject);
    }

    private void Awake()
    {
        incisions = new bool[GetComponentInChildren<EdgeCollider2D>().transform.childCount];

        uncutMat = Resources.Load<Material>(Path.UncutMaterial);
        cutMat = Resources.Load<Material>(Path.Cutmaterial);
    }

    private void OnEnable()
    {
        onIncisionMade += OnIncisionMade;
    }

    private void OnDisable()
    {
        onIncisionMade -= OnIncisionMade;
    }
}
