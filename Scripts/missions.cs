using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions
{
    public JintEvaluator jint;
    // Start is called before the first frame update
    public Missions()
    {
        jint = new JintEvaluator();
    }

    public void test(string code)
    {
        Debug.Log(jint.Evaluate(code).ToString());
    }
}
