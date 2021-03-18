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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (messageText != null && customMessage != null) messageText.text = customMessage; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady && Input.GetKeyDown(KeyCode.Return)) animator.SetTrigger("Enter");
    }

    public void SetMessage(string message)
    {
        customMessage = message;
    }

    public void Ready()
    {
        isReady = true;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
