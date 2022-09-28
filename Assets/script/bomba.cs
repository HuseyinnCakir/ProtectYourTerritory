using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{
    public float guc = 15f;
    public float menzil = 10f;
    public float yukariguc = 1f;
    public ParticleSystem patlamaefekt;
    AudioSource patlamasesi;
    // Start is called before the first frame update
    void Start()
    {
        patlamasesi = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision !=null)
        {
            Destroy(gameObject,.5f);
            
            
            patlama();
        }
    }
    void patlama()
    {
        Vector3 patlamapoz = transform.position;
        Instantiate(patlamaefekt, transform.position, transform.rotation);
        patlamasesi.Play();
        Collider[] colliders = Physics.OverlapSphere(patlamapoz, menzil);
        foreach (Collider  hit in colliders)
        {
            Rigidbody rg = hit.gameObject.GetComponent<Rigidbody>();
            if( hit!=null && rg)
            {
                if (hit.transform.gameObject.CompareTag("dusman"))
                {
                    hit.transform.gameObject.GetComponent<dusman>().oldun();
                }
                rg.AddExplosionForce(guc, patlamapoz, menzil,yukariguc, ForceMode.Impulse);
            }
        }
    }
}
