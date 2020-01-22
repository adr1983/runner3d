using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLane : MonoBehaviour {

	public void PositionLane()
	{
/*		int[]  MyLevelsToLoad = new int[] { 2, 3, 6, 9 };
		int RandomLevelIndex = (int) Math.Min( MyLevelsToLoad.Length-1, Random.Range( 0, MyLevelsToLoad.Length ) );
		Application.LoadLevel( RandomLevelIndex );*/
//		int[] intervalLane = {0, 3};
		int randomLaneX = Random.Range(-2, 3); //dont random # 2
		if (randomLaneX == 1)
		{
			randomLaneX = 0;
		}
		else if (randomLaneX == -1)
		{
			randomLaneX = 0;
		}
		
		float randomLaneY = Random.Range(1, 3); //dont random # 2
		if (randomLaneY > 1 && randomLaneY < 1.5f)
		{
			randomLaneY = 1;
		}
		else if (randomLaneY > 1.5f)
		{
			randomLaneY = 2.5f;
		}
		
//		x deve ser -2 , 0 ou 2
		transform.position = new Vector3(randomLaneX, randomLaneY, transform.position.z);
//		transform.position = new Vector3(randomLaneX, transform.position.y, transform.position.z);
	}
}
