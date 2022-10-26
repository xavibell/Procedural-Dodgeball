using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    private NavMeshAgent nav;
    
    private Rigidbody rigidBody;
    private GameObject player;
    [SerializeField] private float speed;
    private bool ballHit;
    
    private bool stopMoving = false;
    private bool planeCollision;
    

    // Start is called before the first frame update
    void Start()
    {
        
        nav = GetComponent<NavMeshAgent>();
        if (nav)
        {
            nav.enabled = false;
        }
        
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody.isKinematic = true;

        float randomScale = Random.Range(0.4f, 1.2f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        transform.position = transform.position + new Vector3(0, 0.1f, 0);
        planeCollision = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (enemyInitialized())
        {
            moveState();
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Clickable" || collision.gameObject.tag == "Enemy")
        {
            ballHit = true;
        }
        
    }

   

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            planeCollision = false;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Clickable" || other.gameObject.tag == "Enemy")
        {
            ballHit = true;

        }

        if (other.gameObject.tag == "Ground")
        {
            planeCollision = true;
            
        }

        if (other.gameObject.tag == "Player")
        {
            
            if (!ballHit)
            {
                Debug.Log("Game Failed");
                gameFailed();
                Application.LoadLevel(Application.loadedLevel);
            }
            
        }


    }


    private bool enemyInitialized()
    {
        if (planeCollision && !stopMoving)
        {
            transform.position = transform.position + new Vector3(0, 0.01f, 0);
            return false;
        } 
        else
        {
            if (nav)
            {
                nav.enabled = true;
            }
            
            return true;
        }
    }

    private void moveState()
    {

        if (!stopMoving)
        {            
            if (!ballHit)
            {             
                StartCoroutine(moveEnemy());
            }

            else if (ballHit)
            {
                if (nav)
                {
                    Destroy(nav);
                }
                
                rigidBody.isKinematic = false;
                rigidBody.AddForce(new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), Random.Range(1f, -1f)) * 1000);
                rigidBody.AddTorque(new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), Random.Range(1f, -1f)) * 1000);
                ballHit = false;
                GetComponent<Animator>().Play("Deletion");
            }
        }     
    }
            

    public bool gameFailed()
    {       
        stopMoving = true;
        rigidBody.isKinematic = false;
        GetComponent<Animator>().Play("Deletion");
        return true;
    }

    private void Deletion()
    {
        Destroy(this.gameObject);
    }



    IEnumerator moveEnemy()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        if (nav)
        {
            nav.destination = playerPos;
        }
        
        yield return new WaitForSeconds(1);
    }


}
