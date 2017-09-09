using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn : MonoBehaviour {

    public static int levelN = 0;

    private Vector3 startPos;
    private Quaternion starRot;

    // Use this for initialization
    void Start () {

        startPos = transform.position;
        starRot = transform.rotation;	
	}

    void nextlevel()
    {
        levelN++;

        if (levelN > 1)
        {
            levelN = 0;
        }
        
        SceneManager.LoadScene(levelN);
    }

    //detect collision with trigger//
    void OnTriggerEnter(Collider coll) {

        if (coll.tag == "Death")
        {
            transform.position = startPos;
            transform.rotation = starRot;
            GetComponent<Animator>().Play("LOSE00", -1, 0f);
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        }
        else if (coll.tag == "checkpoint")
        {
            startPos = coll.transform.position;
            starRot = coll.transform.rotation;
            Destroy(coll.gameObject);
        }
        else if (coll.tag == "goal")
        {
            Destroy(coll.gameObject);
            GetComponent<Animator>().Play("WIN00", -1, 0f);
            Invoke("nextlevel", 2f);
        }
    }
}
