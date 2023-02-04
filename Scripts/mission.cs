using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

		// mission_table = new Dictionary<string, Mission>();
		// mission_table.Add(fs.GetCurrentDirectory(), new Mission(fs.GetCurrentDirectory()));
		// mission = mission_table[fs.GetCurrentDirectory()];
		// mission.set_statement("place a file called \"abc.js\"\nin the root of this repository", "run submit in the root", "reward print_function");
		// mission.expected = "abcdefghijklmnopqrstuvwxyz";

public class Mission
{
    public JintEvaluator jint;
	public string ip;
	public string statement;
	public string expected;
	public string reward;
	public bool completed;

    public Mission(string m_ip)
    {
        jint = new JintEvaluator();
		ip = m_ip;
		statement = "";
		expected = "";
		completed = false;
    }

	public void set_statement(string problem, string validation, string rewards)
	{
		reward = rewards;
		statement = "MISSION STATEMENT\n" +
							"=================\n" +
							"problem: " + problem  + "\n\n" +
							"validation: " + validation + "\n\n" +
							"reward: " + rewards + "\n";
	}

	public string test(Player player, string[] args)
	{
		if (args.Length == 1)
			return ("usage: mission MACHINE_IP");
		return ("unknown mission\n");
	}
}
