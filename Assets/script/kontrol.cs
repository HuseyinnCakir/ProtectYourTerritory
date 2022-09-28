using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kontrol : MonoBehaviour
{
    public List<GameObject> mermikutusunokta = new List<GameObject>();
    public GameObject mermikutusu;
    public static bool mermikutusu_varmi;
    // Start is called before the first frame update
    void Start()
    {
        mermikutusu_varmi = false;
        StartCoroutine(mermikutusuyap());
    }

    // Update is called once per frame
    IEnumerator mermikutusuyap()
    {
        while (true)
        {
            yield return null;
            if (!mermikutusu_varmi)
            {
                yield return new WaitForSeconds(5f);
                int random = Random.Range(0, 4);
                Instantiate(mermikutusu, mermikutusunokta[random].transform.position, mermikutusunokta[random].transform.rotation);
                mermikutusu_varmi = true;
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
