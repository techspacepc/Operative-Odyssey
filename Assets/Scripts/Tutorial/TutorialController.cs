using System;
using System.Collections;
using System.Collections.Generic;
using Tags;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialController : MonoBehaviour
{
    private XRSocketInteractor traySocket;
    private bool timing = false;
    private float startTime;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Animator animator;
    private readonly List<Func<IEnumerator>> coroutines = new();
    private bool coroutineActive = false;
    private bool table = false;
    private void Awake()
    {
        traySocket = GameObject.FindGameObjectWithTag(Tag.Tray).GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        traySocket.selectEntered.AddListener(OnOrganOnTray);
    }

    private void OnDisable()
    {
        traySocket.selectEntered.RemoveListener(OnOrganOnTray);
    }
    private void Start()
    {
        foreach (CheckTriggerEnter enter in FindObjectsOfType<CheckTriggerEnter>())
        {
            enter.checkTriggerEnterEvent += OnTriggerCheck;
        }
        foreach (SimpleOrganDissect enter in FindObjectsOfType<SimpleOrganDissect>())
        {
            enter.checkCutEvent += OnCut;
        }

    }

    public void OnCut(string obj)
    {
        coroutines.Add(() => OnCut());
        timing = false;
    }

    private IEnumerator OnCut()
    {
        coroutineActive = true;
        animator.SetBool("Talking", true);
        audio.clip = audioClips[8];
        audio.Play();
        yield return new WaitForSeconds(6);
        animator.SetBool("Talking", false); ;
        coroutineActive = false;
        yield return null;
    }

    private void Update()
    {
        if (timing)
        {
            startTime += Time.deltaTime;
            if (startTime > 100f)
            {
                TakeTooLong();
            }

        }

        if (!coroutineActive && coroutines.Count != 0)
        {
            StartCoroutine(coroutines[0].Invoke());
            coroutines.RemoveAt(0);
        }
    }

    private void TakeTooLong()
    {
        timing = false;
        startTime = 0;
        coroutines.Add(() => TooLong());
    }

    private IEnumerator TooLong()
    {
        coroutineActive = true;
        animator.SetBool("Talking", true);
        audio.clip = audioClips[3];
        audio.Play();
        yield return new WaitForSeconds(9);
        animator.SetBool("Talking", false);
        coroutineActive = false;
        yield return null;
    }

    public void OnOrganOnTray(SelectEnterEventArgs args)
    {
        coroutines.Add(() => OrganOnTray());
        startTime = 0;
    }

    private IEnumerator OrganOnTray()
    {
        coroutineActive = true;
        animator.SetBool("Walking", true);
        audio.clip = audioClips[15];
        audio.Play();
        yield return new WaitForSeconds(5);
        animator.SetBool("Walking", false);
        animator.SetBool("Talking", true);
        audio.clip = audioClips[16];
        audio.Play();
        yield return new WaitForSeconds(3);
        animator.SetBool("Talking", false);

        coroutineActive = false;
        yield return null;
    }

    public void OnTriggerCheck(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "TableTrigger":
                if (!table)
                {
                    startTime = 0f;
                    coroutines.Add(() => Table());
                    timing = true;
                }
                break;
        }
    }

    private IEnumerator Table()
    {
        coroutineActive = true;
        table = true;
        animator.SetBool("ComeToLife", true);
        audio.clip = audioClips[0];
        audio.Play();

        yield return new WaitForSeconds(3);
        animator.SetBool("ComeToLife", false);
        animator.SetBool("Talking", true);
        yield return new WaitForSeconds(4);
        audio.clip = audioClips[1];
        audio.Play();

        yield return new WaitForSeconds(6);
        audio.clip = audioClips[2];
        audio.Play();

        yield return new WaitForSeconds(7);
        animator.SetBool("Talking", false);
        coroutineActive = false;
        yield return null;
    }

}
