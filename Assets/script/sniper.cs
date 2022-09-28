using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sniper : MonoBehaviour
{
    Animator animatorum;
    [Header("ayarlar")]
    public bool atesedebilirmi;
    float iceridenatesetmesikligi;
    public float disaridanatesetmesiklik;
    public float menzil;
    public GameObject cross;
    public GameObject scope1;
    [Header("sesler")]
    public AudioSource atessesi;
    public AudioSource jarjorsesi;
    public AudioSource mermibittisesi;
    public AudioSource mermialsesi;
    [Header("efektler")]
    public ParticleSystem efekt;
    public ParticleSystem mermizi;
    public ParticleSystem kanefekt;
    [Header("digerleri")]
    public Camera cam1;
    [Header("silah ayarlar")]
    int toplammermi;
    public float darbegucu;
    public int jarjorkapasite;
    int kalanmermi;
    public string silah_ad;
    public TextMeshProUGUI Toplammermi_text;
    public TextMeshProUGUI kalanmermi_text;
    int atilanmermi;

    float camfieldpov;
    float zoomdeger = 15;
    public bool kovan_ciksinmi;
    public GameObject kovan;
    public GameObject kovancikisnoktasý;
    public kontrol control;

    void Start()
    {
        toplammermi = PlayerPrefs.GetInt(silah_ad+"_mermi")+jarjorkapasite;
        kovan_ciksinmi =true;
        baslangic_mermi_doldur();
        kalanmermi = jarjorkapasite;
        teknik("normalyaz");
        animatorum = GetComponent<Animator>();
        camfieldpov = cam1.fieldOfView;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesedebilirmi && Time.time > iceridenatesetmesikligi && kalanmermi != 0)
            {
                if (!oyunkomplekontrol.oyun_durduruldumu)
                {
                    ateset();
                    iceridenatesetmesikligi = Time.time + disaridanatesetmesiklik;
                }
            }
            if (kalanmermi == 0)
            {
                mermibittisesi.Play();
            }


        }
        if (Input.GetKey(KeyCode.R))
        {
            if (kalanmermi < jarjorkapasite && toplammermi != 0)
                animatorum.Play("sniperjarjor");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            scope(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            scope(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            mermial();
        }
    }
    IEnumerator cameratitret(float sure, float magnitude)
    {
        Vector3 orjinalkonum = cam1.transform.localPosition;
        float gecensure = 0.0f;
        while (gecensure < sure)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            cam1.transform.localPosition = new Vector3(x, orjinalkonum.y, orjinalkonum.z);
            gecensure += Time.deltaTime;
            yield return null;
        }
        cam1.transform.localPosition = orjinalkonum;
    }
    void ateset()
    {
        atesetmeteknik();
        RaycastHit hit;
        if (Physics.Raycast(cam1.transform.position, cam1.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("dusman"))
            {
                Instantiate(kanefekt, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.gameObject.GetComponent<dusman>().darbeal(darbegucu);
            }
            else if (hit.transform.gameObject.CompareTag("devrilebilir"))
            {
                Rigidbody rg = hit.transform.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 50f);
                Instantiate(mermizi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(mermizi, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

    }
    void teknik(string tur)
    {
        switch (tur)
        {
            case "mermivar":
                if (toplammermi <= jarjorkapasite)
                {

                    int toplamolusanmermi = kalanmermi + toplammermi;
                    if (toplamolusanmermi > jarjorkapasite)
                    {
                        kalanmermi = jarjorkapasite;
                        toplammermi = toplamolusanmermi - jarjorkapasite;
                        PlayerPrefs.SetInt(silah_ad + "_mermi", toplammermi);
                    }
                    else
                    {
                        kalanmermi += toplammermi;
                        toplammermi = 0;
                        PlayerPrefs.SetInt(silah_ad + "_mermi", 0);
                    }
                }


                else
                {
                    atilanmermi = jarjorkapasite - kalanmermi;
                    toplammermi -= atilanmermi;
                    kalanmermi = jarjorkapasite;
                    PlayerPrefs.SetInt(silah_ad + "_mermi", toplammermi);
                }

                Toplammermi_text.text = ("/" + toplammermi.ToString());
                kalanmermi_text.text = kalanmermi.ToString();
                break;
            case "mermiyok":
                if (toplammermi <= jarjorkapasite)
                {
                    kalanmermi = toplammermi;
                    toplammermi = 0;
                    PlayerPrefs.SetInt(silah_ad + "_mermi", 0);
                }
                else
                {
                    toplammermi -= jarjorkapasite;
                    kalanmermi = jarjorkapasite;
                    PlayerPrefs.SetInt(silah_ad + "_mermi", toplammermi);
                }

                Toplammermi_text.text = ("/" + toplammermi.ToString());
                kalanmermi_text.text = kalanmermi.ToString();
                break;
            case "normalyaz":
                Toplammermi_text.text = ("/" + toplammermi.ToString());
                kalanmermi_text.text = kalanmermi.ToString();
                break;

        }
    }
    void jarjorsesical()
    {
        jarjorsesi.Play();
        if (kalanmermi < jarjorkapasite && toplammermi != 0)
        {
            if (kalanmermi != 0)
            {
                teknik("mermivar");
            }
            else
            {
                teknik("mermiyok");
            }

        }
    }
    void baslangic_mermi_doldur()
    {
        if (toplammermi <= jarjorkapasite)
        {

            kalanmermi = toplammermi;
            toplammermi = 0;
            PlayerPrefs.SetInt(silah_ad + "_mermi", toplammermi);

        }
        else
        {
            kalanmermi = jarjorkapasite;
            toplammermi -= jarjorkapasite;
            PlayerPrefs.SetInt(silah_ad + "_mermi", toplammermi);
        }
    }
 
    void atesetmeteknik()
    {
        if (kovan_ciksinmi)
        {
            GameObject obje = Instantiate(kovan, kovancikisnoktasý.transform.position, kovancikisnoktasý.transform.rotation);
            Rigidbody rg = obje.GetComponent<Rigidbody>();
            rg.AddRelativeForce(new Vector3(-10, 1, 0) * 60f);
        }
        atessesi.Play();
        efekt.Play();
        StartCoroutine(cameratitret(.10f, .2f));
        animatorum.Play("sniperates");
        kalanmermi--;
        kalanmermi_text.text = kalanmermi.ToString();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("mermi"))
        {
            mermikaydet(other.transform.gameObject.GetComponent<mermikutusu>().olusan_silah_turu, other.transform.gameObject.GetComponent<mermikutusu>().olusan_mermi_sayisi);
            kontrol.mermikutusu_varmi = false;
            Destroy(other.transform.parent.gameObject);

        }
     
        if (other.transform.gameObject.CompareTag("can"))
        {
            control.GetComponent<oyunkomplekontrol>().canal();
            cankutusu.cankutusu_varmi = false;
            Destroy(other.transform.gameObject);

        }
        if (other.transform.gameObject.CompareTag("bombakutusu"))
        {
            control.GetComponent<oyunkomplekontrol>().bombaal();
            bombakutusu.bombakutusu_varmi = false;
            Destroy(other.transform.gameObject);

        }
    }
    void mermial()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam1.transform.position, cam1.transform.forward, out hit, 10))
        {
            if (hit.transform.gameObject.CompareTag("mermi"))
            {
                mermikaydet(hit.transform.gameObject.GetComponent<mermikutusu>().olusan_silah_turu, hit.transform.gameObject.GetComponent<mermikutusu>().olusan_mermi_sayisi);
                kontrol.mermikutusu_varmi = false;
                Destroy(hit.transform.parent.gameObject);

            }
        }
    }
    void mermikaydet(string silahturu, int sayisi)
    {
        mermialsesi.Play();
        switch (silahturu)
        {
            case "taramali":

                PlayerPrefs.SetInt("taramali_mermi", PlayerPrefs.GetInt("taramali_mermi") + sayisi);

                break;
            case "pompali":
                
                PlayerPrefs.SetInt("pompali_mermi",PlayerPrefs.GetInt("pompali_mermi")+sayisi);
                

                break;
            case "sniper":
                toplammermi += sayisi;
                PlayerPrefs.SetInt("sniper_mermi", PlayerPrefs.GetInt("sniper_mermi") + sayisi);
                teknik("normalyaz");
                break;
            case "magnum":
                PlayerPrefs.SetInt("magnum_mermi", PlayerPrefs.GetInt("magnum_mermi") + sayisi);
                break;
        }
    }
    void scope(bool durum)
    {
        if (durum)
        {
            cross.SetActive(false);
            cam1.cullingMask = ~(1 << 6);
            scope1.SetActive(true);
            animatorum.SetBool("zoom", durum);
            cam1.fieldOfView = zoomdeger;
        }
        else
        {
            scope1.SetActive(false);
            cam1.cullingMask = -1;
            cross.SetActive(true);
            animatorum.SetBool("zoom", durum);
            cam1.fieldOfView = camfieldpov;
        }
        
    }
}