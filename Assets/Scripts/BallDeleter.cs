using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeleter : MonoBehaviour
{
    private bool timerStart;
    private int timer;
    private GameObject ballSpawnPosition;
    [SerializeField] private GameObject ball;
    private GameObject player;
    private GameObject ballParent;


    // Start is called before the first frame update
    void Start()
    {
        timerStart = false;
        ballSpawnPosition = GameObject.FindGameObjectWithTag("BallSpawnPos");
        ballParent = GameObject.FindGameObjectWithTag("BallParent");
        player = GameObject.FindGameObjectWithTag("Player");
        this.enabled = true;
        this.gameObject.SetActive(true);
        gameObject.transform.parent = ballParent.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerStart)
        {
            Debug.Log("DELETING");
            timer++;

            if (timer > 10)
            {
                Instantiate(ball, new Vector3(Random.Range(-38f, 38f), 10, Random.Range(-100f, 100)), Quaternion.Euler(0, 0, 0));
                Destroy(this.gameObject);
            }

        }

        if (Vector3.Distance(this.transform.position, player.transform.position) > 60)
        {
            timerStart = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timerStart = true;
            
        }
    }

}
