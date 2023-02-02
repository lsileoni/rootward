using UnityEngine;
using Jint;
using Jint.Native;
using System;

public class JintEvaluator
{
    private Engine engine;

    public JintEvaluator()
    {
        engine = new Engine().SetValue("log", new Action<object>(Debug.Log));
    }

    public JsValue Evaluate(string code)
    {
        //test
        return engine.Execute(code).GetCompletionValue();
    }
}