using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteInteraction : MonoBehaviour
{
    [SerializeField] private ParticleSystem PS;
    [SerializeField] private GameActionSequence misInput;

    public ComponentType note;
    private Controls pInput;

    private bool holdNote, noteHit;
    [SerializeField] private GameObject note1, note2, note3;

    private void Start()
    {
        pInput = new Controls();
        pInput.Enable();

        if (note == ComponentType.noteA)
        {
            pInput.Player.NoteA.performed += ButtonPressed;
            pInput.Player.NoteA.canceled += ButtonReleased;
        }
        else if (note == ComponentType.noteB)
        {
            pInput.Player.NoteB.performed += ButtonPressed;
            pInput.Player.NoteB.canceled += ButtonReleased;

        }
        else if (note == ComponentType.noteC)
        {
            pInput.Player.NoteC.performed += ButtonPressed;
            pInput.Player.NoteC.canceled += ButtonReleased;
        }
    }
    
    private void OnDisable()
    {
        pInput.Disable();
        if (note == ComponentType.noteA)
        {
            pInput.Player.NoteA.performed -= ButtonPressed;
            pInput.Player.NoteA.canceled -= ButtonReleased;
        }
        else if (note == ComponentType.noteB)
        {
            pInput.Player.NoteB.performed -= ButtonPressed;
            pInput.Player.NoteB.canceled -= ButtonReleased;
        }
        else if (note == ComponentType.noteC)
        {
            pInput.Player.NoteC.performed -= ButtonPressed;
            pInput.Player.NoteC.canceled -= ButtonReleased;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HoldNote")
        {
            if (holdNote)
            {
                holdNote = false;
            }
            else
            {
                holdNote = true;
            }
        }
        //StartCoroutine(nameof(holdNote));
        collision.GetComponent<GameActionSequence>().Play();
    }

    private void ButtonPressed(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(EnableCollider));
        PS.Play();

        if (note == ComponentType.noteA)
        {
            note1.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (note == ComponentType.noteB)
        {
            note2.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (note == ComponentType.noteC)
        {
            note3.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void ButtonReleased(InputAction.CallbackContext c)
    {
        if (holdNote)
        {
            StartCoroutine(nameof(EnableCollider));
        }
        if (note == ComponentType.noteA)
        {
            note1.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (note == ComponentType.noteB)
        {
            note2.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (note == ComponentType.noteC)
        {
            note3.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    IEnumerator NoteHit()
    {
        noteHit = true;
        yield return new WaitForSeconds(.1f);
        noteHit = false;
    }

    IEnumerator EnableCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(.1f);

        if (!noteHit)
        {
            Debug.Log("miss");
            if (misInput)
            {
                misInput.Play();
            }
        }

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}

public enum ComponentType {noteA, noteB, noteC}