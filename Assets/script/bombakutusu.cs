using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombakutusu : MonoBehaviour
{
    public List<GameObject> bombakutusunokta = new List<GameObject>();
    public GameObject bombakutusu1;
    public static bool bombakutusu_varmi;
    // Start is called before the first frame update
    void Start()
    {
        bombakutusu_varmi = false;
        StartCoroutine(bombakutusuyap());
    }

    // Update is called once per frame
    IEnumerator bombakutusuyap()
    {
        while (true)
        {
            yield return null;
            if (!bombakutusu_varmi)
            {
                yield return new WaitForSeconds(2f);
                int random = Random.Range(0, 4);
                Instantiate(bombakutusu1, bombakutusunokta[random].transform.position, bombakutusunokta[random].transform.rotation);
                bombakutusu_varmi = true;
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
