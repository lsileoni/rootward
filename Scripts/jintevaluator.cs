using UnityEngine;
using Jint;
using Jint.Native;
using System;

public class JintEvaluator
{
    private Engine engine;



    string log_return(string output)
    {
        return (output);
    }
    public JintEvaluator()
    {
        engine = new Engine().SetValue("log", new Action<object>(log_return));
    }

    public JsValue Evaluate(string code)
    {
        return engine.Execute(code).GetCompletionValue();
    }
}