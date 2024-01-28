using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteInteraction : MonoBehaviour
{
    public ComponentType note;
    private Controls pInput;

    private bool holdNote;

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
        collision.GetComponent<GameActionSequence>().Play();
    }

    private void ButtonPressed(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(EnableCollider));
    }

    private void ButtonReleased(InputAction.CallbackContext c)
    {
        if (holdNote)
        {
            StartCoroutine(nameof(EnableCollider));
        }
    }

    IEnumerator EnableCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}

public enum ComponentType {noteA, noteB, noteC}