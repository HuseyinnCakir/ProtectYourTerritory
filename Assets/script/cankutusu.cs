using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cankutusu : MonoBehaviour
{
    public List<GameObject> cankutusunokta = new List<GameObject>();
    public GameObject cankutusu1;
    public static bool cankutusu_varmi;
    // Start is called before the first frame update
    void Start()
    {
        cankutusu_varmi = false;
        StartCoroutine(cankutusuyap());
    }

    // Update is called once per frame
    IEnumerator cankutusuyap()
    {
        while (true)
        {
            yield return null;
            if (!cankutusu_varmi)
            {
                yield return new WaitForSeconds(2f);
                int random = Random.Range(0, 4);
                Instantiate(cankutusu1, cankutusunokta[random].transform.position, cankutusunokta[random].transform.rotation);
                cankutusu_varmi = true;
            }
           
        }
        /* while (true)
         {
             yield return new WaitForSeconds(5f);
             int random = Random.Range(0,4);
             Instantiate(mermikutusu, mermikutusunokta[random].transform.position, mermikutusunokta[random].transform.rotation);
             mermikutusu_varmi = true;
         }*/
    }
}
