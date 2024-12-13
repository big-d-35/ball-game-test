using UnityEngine;
using UnityEngine.UI;
using System;
public class Ball : MonoBehaviour
{
    public Action<bool> onThrow;
    private Rigidbody rb;
    public static Ball instance;
    [HideInInspector]
    public bool hit;
    Vector3 startPos;
    Vector3 startRot;
    [HideInInspector]
    public Slider slider;
    public AudioSource netSound;
    public AudioSource throwSound;
    private void Awake()
    {
        instance = this;
        startPos = transform.position;
        startRot = transform.eulerAngles;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Throw()
    {
        throwSound.Play();
        rb.isKinematic = false;
        rb.AddForce(transform.up * slider.value, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            transform.position = startPos;
            transform.eulerAngles = startRot;
            rb.isKinematic = true;
            if (!hit)
            {
                onThrow?.Invoke(hit);
            }
            hit = false;
        }
        if (other.gameObject.CompareTag("Hit"))
        {
            netSound.Play();
            hit = true;
            onThrow?.Invoke(hit);
        }
    }
}