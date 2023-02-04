using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mission
{
    public JintEvaluator jint;
	public string ip;
	public string statement;

    public Mission(string m_ip)
    {
        jint = new JintEvaluator();
		ip = m_ip;
		statement = "";
    }
	public void set_statement(string problem, string validation, string reward)
	{
		statement = "MISSION STATEMENT\n" +
							"=================\n" +
							"problem: " + problem  + "\n\n" +
							"validation: " + validation + "\n\n" +
							"reward: " + reward + "\n";
	}

    public bool available_mission(string m_ip)
	{
		return (ip == m_ip);
	}

	public string test(Player player, string[] args)
	{
		if (args.Length == 1)
			return ("usage: mission MACHINE_IP");
		// if (available_mission(args[1]) && args.Length == 2)
		// 	return (mission_statement(args[1]));
		return ("unknown mission\n");
	}
}
