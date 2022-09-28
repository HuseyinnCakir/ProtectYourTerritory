using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boskovan : MonoBehaviour
{
    AudioSource yeredusmesesi;
    // Start is called before the first frame update
    void Start()
    {
        
        yeredusmesesi = GetComponent<AudioSource>();
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("yol"))
        {
            yeredusmesesi.Play();
            if (!yeredusmesesi.isPlaying)
            {
                Destroy(gameObject, 1f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
