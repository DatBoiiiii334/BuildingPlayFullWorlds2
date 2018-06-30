using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Text logText;
    public string displayText = "Type your text here";
    public int displayTime;
    public bool destroyOnClose = false;

    public bool isDisplaying { get; set; }

    private void Start()
    {
        logText.text = "";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && isDisplaying)
        {
            Close();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Popup());
        }
    }

    void Close()
    {
        isDisplaying = false;
        if (destroyOnClose)
        {
            Destroy(this);
        }
    }

    IEnumerator Popup()
    {
        isDisplaying = true;
        logText.text = displayText;
        yield return new WaitForSeconds(displayTime);
        Close();
    }
}