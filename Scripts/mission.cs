using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public JintEvaluator jint;
	public ArrayList open_ips;

    public Mission()
    {
        jint = new JintEvaluator();
		open_ips = new ArrayList();
		open_ips.Add("42");
		open_ips.Add("69");
    }


    public bool available_mission(string ip)
	{
		return (open_ips.Contains(ip));
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
		if (args.Length == 1)
			return ("usage: mission MACHINE_IP");
		if (available_mission(args[1]) && args.Length == 2)
			return (mission_statement(args[1]));
		return ("unknown mission\n");
	}
}
