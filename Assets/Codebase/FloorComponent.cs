using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase
{
    public class FloorComponent : MonoBehaviour
    {
        private Rigidbody _ballRB = null;

        private void Update()
        {
            if (_ballRB != null && _ballRB.linearVelocity.magnitude == 0)
                StartCoroutine(ReloadLevel());
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Ball")
                _ballRB = other.gameObject.GetComponent<Rigidbody>();
        }

        private IEnumerator ReloadLevel()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
        }
    }    
}