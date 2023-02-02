using UnityEngine;
using Jint;
using Jint.Native;

public class JintEvaluator
{
    private Engine engine;

    public JintEvaluator()
    {
        engine = new Engine();
    }

    public JsValue Evaluate(string code)
    {
        return engine.Execute(code).GetCompletionValue();
    }
}