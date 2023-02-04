using UnityEngine;
using Jint;
using Jint.Native;
using System;
using TMPro;

public class JintEvaluator : MonoBehaviour
{
    private Engine engine;
    private TMP_InputField inputField;

    public JintEvaluator(TMP_InputField inputField)
    {
        engine = new Engine().SetValue("log", new Action<object>(Debug.Log));
        engine.SetValue("println", new Action<object>(x => inputField.text += x.ToString() + "\n"));
        engine.SetValue("print", new Action<object>(x => inputField.text += x.ToString()));
        this.inputField = inputField;
    }
    public JintEvaluator()
    {
        engine = new Engine().SetValue("log", new Action<object>(Debug.Log));
        engine.SetValue("println", new Action<object>(x => inputField.text += x.ToString() + "\n"));
        engine.SetValue("print", new Action<object>(x => inputField.text += x.ToString()));
    }    

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public JsValue Evaluate(string code)
    {
        return engine.Execute(code).GetCompletionValue();
    }
}
