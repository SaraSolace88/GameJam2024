using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteInteraction : MonoBehaviour
{
    public ComponentType note;
    private Controls pInput;

    private void Start()
    {
        pInput = new Controls();
        pInput.Enable();

        if (note == ComponentType.noteA)
        {
            pInput.Player.NoteA.performed += ButtonPressed;
        }
        else if (note == ComponentType.noteB)
        {
            pInput.Player.NoteB.performed += ButtonPressed;

        }
        else if (note == ComponentType.noteC)
        {
            pInput.Player.NoteC.performed += ButtonPressed;
        }
    }
    
    private void OnDisable()
    {
        pInput.Disable();
        if (note == ComponentType.noteA)
        {
            pInput.Player.NoteA.performed -= ButtonPressed;
        }
        else if (note == ComponentType.noteB)
        {
            pInput.Player.NoteB.performed -= ButtonPressed;

        }
        else if (note == ComponentType.noteC)
        {
            pInput.Player.NoteC.performed -= ButtonPressed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void ButtonPressed(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(EnableCollider));
    }


    IEnumerator EnableCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}

public enum ComponentType {noteA, noteB, noteC}