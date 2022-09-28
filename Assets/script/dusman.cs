using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dusman : MonoBehaviour
{
    NavMeshAgent ajan;
    GameObject hedef;
    public float dusmandarbegucu;
    GameObject kontrolcum;
    Animator anim;

    public float healty;
    void Start()
    {
        kontrolcum = GameObject.FindWithTag("anakontrolcum");
        ajan = GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ajan.SetDestination(hedef.transform.position);
    }
    public void hedefbelirle(GameObject obje)
    {
        hedef = obje;
    }
    public void darbeal(float darbegucu)
    {
        healty -= darbegucu;
        if (healty <= 0)
        {
            oldun();
            gameObject.tag = "Untagged";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("hedefimiz"))
        {
            
            kontrolcum.GetComponent<oyunkomplekontrol>().darbeal(dusmandarbegucu);
            oldun();
        }
    }
    public void oldun()
    {
        anim.SetTrigger("ölme");
        Destroy(gameObject, 5f);
        kontrolcum.GetComponent<oyunkomplekontrol>().dusmansayisi_guncelle();
    }
}
