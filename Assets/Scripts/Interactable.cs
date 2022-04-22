using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string InteractDescription;
    public UnityEvent OnInteract;

    public void Interact()
    {
        if(OnInteract != null)
        {
            OnInteract.Invoke();
        }
    }
    
}
