using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		new_mission.set_statement("change the value of \"byte42.data\" to 00101010", "'man cd' 'man ed'", "inprogress");
		mission_table.Add(new_mission.ip, new_mission);

		new_mission = new Mission("248.185.51.148");
		new_mission.set_statement("edit \"hello_world.js\" so its syntax is valid", "'js hello_world.js' javascript statements end in ;", "inprogress");
		mission_table.Add(new_mission.ip, new_mission);

		new_mission = new Mission("136.13.38.91");
		new_mission.set_statement("create a javascript script named \"num_loop.js\"\n\tthis script should print each number from 0 to 100\n\tseparated by a newline",
									"for loop, println()", "inprogress");
		mission_table.Add(new_mission.ip, new_mission);

	}

	public void set_statement(string problem, string tip, string rewards)
	{
		reward = rewards;
		statement = "MISSION STATEMENT\n" +
							"=================\n" +
							"problem : " + problem  + "\n" +
							"tips    : " + tip + "\n" +
							"reward. : " + rewards + "\n";
	}
}
