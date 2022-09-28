using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mermikutusu : MonoBehaviour
{
    string[] silahlar = { "magnum", "sniper", "taramali", "pompali" };
    int[] mermisayisi = { 10, 20, 30, 40 };
    public string olusan_silah_turu;
    public int olusan_mermi_sayisi;
    public List<Sprite> silah_resim = new List<Sprite>();
    public Image silahin_resmi;
   
    void Start()
    {
        
        int gelen = Random.Range(0, silahlar.Length);
        olusan_silah_turu = silahlar[gelen];
        olusan_mermi_sayisi = mermisayisi[Random.Range(0, silahlar.Length - 1)];
        silahin_resmi.sprite = silah_resim[gelen];
       
    }
    
    
    
   
}
