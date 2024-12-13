using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Balistika : MonoBehaviour
{
    public KeyCode Space;
    Rigidbody rb;
    public float y;
    public float x;
    public bool zemla;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    IEnumerator LeafDestruction()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }


    void Update()
    {
        if (zemla == true && Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 2*y, 2*x), ForceMode.Force);
            zemla = false;
            this.GetComponent<Rigidbody>().useGravity = true;

        }
          

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "leaf1")
        {
            StartCoroutine(LeafDestruction());
        }
    }

}
