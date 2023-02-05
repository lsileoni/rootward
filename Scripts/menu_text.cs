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
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			load_scene();
	}

    void load_scene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/repl_scene");
    }
}