using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_text : MonoBehaviour
{
    public Button test;

    void Start()
    {
        test.onClick.AddListener(load_scene);
    }

    void load_scene()
    {
        Debug.Log("alkddlfelkeflkjeflkjalk");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/SampleScene");
    }
}