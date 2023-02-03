using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int current_mission;
	public int level;
	public ArrayList functions;

	public Player()
	{
		current_mission = 0;
		level = 0;
		functions = new ArrayList();
	}

}
