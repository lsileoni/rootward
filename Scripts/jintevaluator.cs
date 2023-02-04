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
        engine = new Engine(options =>
        {
            options.TimeoutInterval(TimeSpan.FromSeconds(4));
            options.MaxStatements(1000);
        }).SetValue("log", new Action<object>(Debug.Log));
        engine.SetValue("println", new Action<object>(x => inputField.text += x.ToString() + "\n"));
        engine.SetValue("print", new Action<object>(x => inputField.text += x.ToString()));
        this.inputField = inputField;
    }
    public JintEvaluator()
    {
        engine = new Engine(options =>
        {
            options.TimeoutInterval(TimeSpan.FromSeconds(4));
            options.MaxStatements(1000);
        }).SetValue("log", new Action<object>(Debug.Log));
        engine.SetValue("println", new Action<object>(x => inputField.text += x.ToString() + "\n"));
        engine.SetValue("print", new Action<object>(x => inputField.text += x.ToString()));
    }

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public JsValue Evaluate(string code, TMP_InputField inputField)
    {
        JsValue intermediate_result = null;
        try
        {
            intermediate_result = engine.Execute(code).GetCompletionValue();
        }
        catch
        {
            inputField.text += "Invalid Javascript";
        }
        return (intermediate_result);
    }
}
