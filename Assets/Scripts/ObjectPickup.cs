using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private GameObject pickupPos;
    private int layerMask;
    private bool clicked;
   
    private bool isChild;
    void Start()
    {
        layerMask = LayerMask.GetMask("Clickable");
        clicked = false;
    }

    // Update is called once per frame
    void Update()

    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Mouse0)) { clicked = true; }

        if (Input.GetKeyUp(KeyCode.Mouse0)) { clicked = false; }



        if (Physics.Raycast(ray, out hit, 3, layerMask))
        {           

            if (clicked && !isChild)
            {
                hit.transform.position = pickupPos.transform.position;
                hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                hit.transform.parent = pickupPos.transform;
                isChild = true;      
            }

            if (!clicked && isChild)
            {
                hit.transform.GetComponent<Rigidbody>().isKinematic = false;

                hit.transform.parent = null;

                Vector3 posA = this.transform.position;
                Vector3 posB = hit.transform.position;
                //Destination - Origin
                Vector3 dir = (posB - posA).normalized;
                hit.transform.GetComponent<Rigidbody>().AddForce(dir * 1000);
                isChild = false;

            }
        }
    }  
}
