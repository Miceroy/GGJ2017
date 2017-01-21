using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextNotifier : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public void show()
    {
        GetComponent<Text>().text = "Head Shot!!";
        Invoke("hide", 1.0f);
    }

    void hide()
    {
        GetComponent<Text>().text = "";
    }


}
