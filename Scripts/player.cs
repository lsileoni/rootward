using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int level;
	public ArrayList functions;
	public Mission current_mission;

	public Player()
	{
		level = 0;
		functions = new ArrayList();
		current_mission = null;
	}

}
