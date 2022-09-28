using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anamenu : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingslider;
    // Start is called before the first frame update
    public void oyunabasla()
    {
        StartCoroutine(sahneyukle());
    }
    IEnumerator sahneyukle()
    {
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(1);
            loadingPanel.SetActive(true);
            while (!operation.isDone)
            {
                float ilerleme = Mathf.Clamp01(operation.progress / .9f);
                loadingslider.value = ilerleme;
                yield return null;
            }
        }
    }
    public void oyundancik()
    {
        Application.Quit();
    }
}
