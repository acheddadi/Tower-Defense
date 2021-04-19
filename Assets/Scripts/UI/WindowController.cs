// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to control animated window objects which
// are used to display popup messages for the user to read.
// --------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    [SerializeField] private Text messageText;

    private string customMessage;
    private Animator animator;
    private bool isReady = false;

    // Set custom message.
    void Start()
    {
        animator = GetComponent<Animator>();
        if (messageText != null && customMessage != null) messageText.text = customMessage; 
    }

    // Update is called once per frame
    void Update()
    {
        // If user hit the enter key, close the popup.
        if (isReady && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            animator.SetTrigger("Enter");
            AudioController.Confirm();
        }
    }

    // Helper method to set the message.
    public void SetMessage(string message)
    {
        customMessage = message;
    }

    public void SetDelay(float delay)
    {
        StartCoroutine(DelayedStart(delay));
    }

    private IEnumerator DelayedStart(float delay)
    {
        yield return new WaitForEndOfFrame();
        animator.speed = 0.0f;
        yield return new WaitForSeconds(delay);
        animator.speed = 1.0f;
    }

    // Helper method to let us know that the window is ready to be interacted with.
    public void Ready()
    {
        isReady = true;
    }

    // Helper method to let us know that the animation is done playing, and we can destroy the window.
    public void Close()
    {
        Destroy(gameObject);
    }
}
