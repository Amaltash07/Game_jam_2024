using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class interactor : MonoBehaviour

{
    public Transform intreactorsorce;
    public float interactorrange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Ray r = new Ray(intreactorsorce.position, intreactorsorce.forward);
            if(Physics.Raycast(r,out RaycastHit hitInfo,interactorrange))
            {
              if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
