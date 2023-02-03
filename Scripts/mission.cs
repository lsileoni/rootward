using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public JintEvaluator jint;
	public string open_ips;
    // Start is called before the first frame update
    public Mission()
    {
        jint = new JintEvaluator();
		open_ips = "42 69";
    }


    public bool available_mission(string ip)
	{
		if (open_ips.Contains(ip) == true)
		{
			return (true);
		}
		return (false);
	}

	public string mission_statement(string ip)
	{
		string statement = "MISSION STATEMENT\n" +
							"=================\n" +
							"problem: " +  "big problem\n\n" +
							"validation: " + "no clue\n\n" +
							"reward: " + "great reward\n";
		return (statement);
	}
	public string test(Player player, string[] args)
	{
		if (available_mission(args[1]) && args.Length == 2)
		{
			return (mission_statement(args[1]));
		}
		return ("succes\n");
	}
    // public string expected_out(Player player)
    // {
    //     if (player.current_mission == 0)
    //     {
    //         return ("helloworld");
    //     }
    //     return ("");
    // }
    // public string test(Player player, string[] args)
    // {
    //     string output = jint.Evaluate(code).ToString();
    //     string solution = expected_out(player).ToString();
    //     if (output.Equals(solution))
    //     {
	// 		return (true);
    //     }
    //     Debug.Log("expected " + expected_out(player));
    //     Debug.Log("output " + output);
	// 	return (false);
    // }
}
