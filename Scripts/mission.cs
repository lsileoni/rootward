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

	public void init_missions(Dictionary<string, Mission> mission_table)
	{
		Mission new_mission = new Mission("93.1.183.174");
		new_mission.set_statement("no problem", "submit", "inprogress");
		mission_table.Add(new_mission.ip, new_mission);

		new_mission = new Mission("248.185.51.148");
		new_mission.set_statement("create a file called \"file.txt\"\ncontaining the following byte \"00101010\"", "submit", "inprogress");
		mission_table.Add(new_mission.ip, new_mission);

		mission_table.Add("136.13.38.91", new Mission("136.13.38.91"));
		mission_table.Add("228.109.159.41", new Mission("228.109.159.41"));

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
