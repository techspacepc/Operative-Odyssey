using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganDissect : MonoBehaviour
{
    [SerializeField] private Transform left, right;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    private void Start()
    {
        left.position = transform.position + offset;
        right.position = transform.position - offset;
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
