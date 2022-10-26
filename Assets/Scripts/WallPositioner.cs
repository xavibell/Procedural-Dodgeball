using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPositioner : MonoBehaviour
{
    [HideInInspector] public bool colliding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void RandomisePosition()
    {
        int layerMask = ~LayerMask.GetMask("Ground");
        int randomNum;
        int randomRot;
        randomNum = Random.Range(0, 2);

        if (randomNum == 0) { randomRot = 90; } else { randomRot = 0; }
    
        transform.position = new Vector3(Random.Range(-38f, 38f), 0, Random.Range(-100f, 100));
        transform.rotation = Quaternion.Euler(0, randomRot, 0);
        transform.localScale += new Vector3(Random.Range(0.01f, 0.2f), Random.Range(1f, 5f), Random.Range(0f, 5f));


        StartCoroutine(findPosition());
        

    }

    IEnumerator findPosition()
    {
        int layerMask = ~LayerMask.GetMask("Ground");
        int randomNum;
        int randomRot;
        randomNum = Random.Range(0, 2);

        while (Physics.OverlapSphere(transform.position, 1f, layerMask).Length > 0)
        {
            randomNum = Random.Range(0, 2);

            if (randomNum == 0) { randomRot = 90; } else { randomRot = 0; }

            transform.position = new Vector3(Random.Range(-38f, 38f), 0, Random.Range(-100f, 100));
            transform.rotation = Quaternion.Euler(0, randomRot, 0);         
        }

        yield return null;
    }

}
