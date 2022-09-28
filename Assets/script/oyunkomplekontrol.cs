using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class oyunkomplekontrol : MonoBehaviour
{
    [Header("saglýk ayar")]
    float healty = 100;
    public Image healtybar;
    [Header("silah ayar")]
    public GameObject[] silahlar;
    public AudioSource degisimses;
    public GameObject bomba;
    public GameObject bombacikis;
    public Camera benimcam;
    [Header("dusman ayar")]
    public GameObject[] dusmanlar;
    public GameObject[] hedefnoktalar;
    public GameObject[] cikisnoktalar;
    public int dusmansayisi;
    public TextMeshProUGUI kalandusman_text;
    public static int kalandusmansayisi;
    [Header("DÝGER ayar")]
    public GameObject lose;
    public GameObject win;
    public GameObject durdurma;
    public AudioSource oyunicises;
    public TextMeshProUGUI sagliksayisi_text;
    public TextMeshProUGUI bombasayisi_text;
    public static bool oyun_durduruldumu;
    // Start is called before the first frame update
    void Start()
    {
        baslangicislemler();
    }
    void baslangicislemler()
    {
        oyun_durduruldumu = false;
        if (!PlayerPrefs.HasKey("oyun_basladimi"))
        {
            PlayerPrefs.SetInt("taramali_mermi", 90);
            PlayerPrefs.SetInt("pompali_mermi", 40);
            PlayerPrefs.SetInt("sniper_mermi", 20);
            PlayerPrefs.SetInt("magnum_mermi", 30);
            PlayerPrefs.SetInt("sagliksayisi", 1);
            PlayerPrefs.SetInt("bombasayisi", 5);
            PlayerPrefs.SetInt("oyun_basladimi", 1);
        }

        oyunicises = GetComponent<AudioSource>();
        oyunicises.Play();
        kalandusman_text.text = dusmansayisi.ToString();
        kalandusmansayisi = dusmansayisi;
        sagliksayisi_text.text = PlayerPrefs.GetInt("sagliksayisi").ToString();
        bombasayisi_text.text = PlayerPrefs.GetInt("bombasayisi").ToString();
        PlayerPrefs.SetInt("taramali_mermi", 150);
        silahlar[0].SetActive(true);
        StartCoroutine(dusmancikar());
    }
    public void dusmansayisi_guncelle()
    {
        kalandusmansayisi--;
        if (kalandusmansayisi <= 0)
        {
            win.SetActive(true);
            kalandusman_text.text = "0";
            Time.timeScale = 0;
        }
        else
        {
            kalandusman_text.text = kalandusmansayisi.ToString();
        }

    }
    IEnumerator dusmancikar()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (dusmansayisi != 0)
            {
                yield return new WaitForSeconds(2f);
                int dusman = Random.Range(0, 5);
                int cikisnokta = Random.Range(0, 2);
                int hedefnokta = Random.Range(0, 2);
                GameObject obje = Instantiate(dusmanlar[dusman], cikisnoktalar[cikisnokta].transform.position, Quaternion.identity);
                obje.GetComponent<dusman>().hedefbelirle(hedefnoktalar[hedefnokta]);
                dusmansayisi--;
            }
            else
            {
                break;
            }

        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !oyun_durduruldumu)
        {
            silahdegis(0);


        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !oyun_durduruldumu)
        {
            silahdegis(1);


        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !oyun_durduruldumu)
        {
            silahdegis(2);


        }
        if (Input.GetKeyDown(KeyCode.G) && !oyun_durduruldumu)
        {
            bombaat();


        }
        if (Input.GetKeyDown(KeyCode.E) && !oyun_durduruldumu)
        {
            canver();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !oyun_durduruldumu)
        {
            pause();
        }
    }
    void silahdegis(int num)
    {
        foreach (var silah in silahlar)
        {
            silah.SetActive(false);
        }
        degisimses.Play();
        silahlar[num].SetActive(true);
    }
    public void darbeal(float darbegucu)
    {
        healty -= darbegucu;
        healtybar.fillAmount = healty / 100;
        if (healty <= 0)
        {
            gameover();
        }

    }
    void gameover()
    {
        lose.SetActive(true);
        Time.timeScale = 0;
        oyun_durduruldumu = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;

    }
    public void tekrarbasla()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        oyun_durduruldumu = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
    }
    public void anamenu()
    {
        SceneManager.LoadScene(0);
    }
    public void pause()
    {
        durdurma.SetActive(true);
        Time.timeScale = 0;
        oyun_durduruldumu = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
    }
    public void devamet()
    {
        durdurma.SetActive(false);
        Time.timeScale = 1;
        oyun_durduruldumu = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
    }
    public void canver()
    {
        if (PlayerPrefs.GetInt("sagliksayisi") != 0 && healty!=100)
        {
            healty = 100;
            healtybar.fillAmount = healty / 100;
            PlayerPrefs.SetInt("sagliksayisi", PlayerPrefs.GetInt("sagliksayisi") - 1);
            sagliksayisi_text.text = PlayerPrefs.GetInt("sagliksayisi").ToString();
        }
    }
    public void canal()
    {
        PlayerPrefs.SetInt("sagliksayisi", PlayerPrefs.GetInt("sagliksayisi") + 1);
        sagliksayisi_text.text = PlayerPrefs.GetInt("sagliksayisi").ToString();
    }
    public void bombaal()
    {
        PlayerPrefs.SetInt("bombasayisi", PlayerPrefs.GetInt("bombasayisi") + 1);
        bombasayisi_text.text = PlayerPrefs.GetInt("bombasayisi").ToString();
    }
    void bombaat()
    {
        if (PlayerPrefs.GetInt("sagliksayisi") != 0)
        {
            GameObject obje = Instantiate(bomba, bombacikis.transform.position, bombacikis.transform.rotation);
            Rigidbody rg = obje.GetComponent<Rigidbody>();
            Vector3 acimiz = Quaternion.AngleAxis(90, benimcam.transform.forward) * benimcam.transform.forward;
            rg.AddForce(acimiz * 250f);
            PlayerPrefs.SetInt("bombasayisi", PlayerPrefs.GetInt("bombasayisi") - 1);
            bombasayisi_text.text = PlayerPrefs.GetInt("bombasayisi").ToString();
        }
    }
}
