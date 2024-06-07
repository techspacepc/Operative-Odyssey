using Pathing;
using System;
using UnityEngine;

public class IncisionManager : MonoBehaviour
{
    [SerializeField] private GameObject cutOrgan;

    public static Action<int> onIncisionMade;
    public static event Action OnIncisionFailed;
    public static Material uncutMat, cutMat;
    private static bool[] incisions;

    private int lastIncisionIndex;

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
    }

    private void Cut()
    {
        Instantiate(cutOrgan, transform.position, Quaternion.identity, transform.parent);

        Destroy(gameObject);
    }

    private void Awake()
    {
        Transform transform = GetComponentInChildren<EdgeCollider2D>().transform;

        incisions = new bool[transform.childCount];

        uncutMat = Resources.Load<Material>(Path.UncutMaterial);
        cutMat = Resources.Load<Material>(Path.Cutmaterial);

        foreach (Transform child in transform)
            child.gameObject.AddComponent<Incision>();
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
