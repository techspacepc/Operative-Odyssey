using System.Collections;
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
    private readonly Coroutine[] coroutines;
    private readonly bool coroutineActive = false;
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

        StartCoroutine(OnCut());
    }

    private IEnumerator OnCut()
    {
        audio.clip = audioClips[8];
        audio.Play();

        yield return null;
    }

    private void Update()
    {
        if (timing)
        {
            startTime += Time.deltaTime;
            if (startTime > 30f)
            {
                TakeTooLong();
            }

        }
    }

    private void TakeTooLong()
    {
        timing = false;
        startTime = 0;
        StartCoroutine(TooLong());
    }

    private IEnumerator TooLong()
    {
        audio.clip = audioClips[3];
        audio.Play();
        yield return null;
    }

    public void OnOrganOnTray(SelectEnterEventArgs args)
    {
        StartCoroutine(OrganOnTray());
        startTime = 0;
    }

    private IEnumerator OrganOnTray()
    {
        audio.clip = audioClips[4];
        audio.Play();
        yield return new WaitForSeconds(2);
        audio.clip = audioClips[5];
        audio.Play();
        yield return new WaitForSeconds(6);
        audio.clip = audioClips[6];
        audio.Play();

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
                    StartCoroutine(Table());
                    timing = true;
                }
                break;
        }
    }

    private IEnumerator Table()
    {
        table = true;
        audio.clip = audioClips[0];
        audio.Play();

        yield return new WaitForSeconds(7);
        audio.clip = audioClips[1];
        audio.Play();

        yield return new WaitForSeconds(6);
        audio.clip = audioClips[2];
        audio.Play();

        yield return null;
    }

}
