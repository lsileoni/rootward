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

    public string expected_out(Player player)
    {
        if (player.current_mission == 0)
        {
            return ("helloworld");
        }
        return ("");
    }
    public bool test(string code, Player player)
    {
        string output = jint.Evaluate(code).ToString();
        string solution = expected_out(player).ToString();
        if (output.Equals(solution))
        {
			return (true);
        }
        Debug.Log("expected " + expected_out(player));
        Debug.Log("output " + output);
		return (false);
    }
}
