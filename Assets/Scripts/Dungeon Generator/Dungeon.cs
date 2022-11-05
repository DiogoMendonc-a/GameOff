using System.Collections.Generic;
using UnityEngine;

public class Dungeon {
	public readonly int dungeonSeed;
	List<Level> levels;

	public Dungeon (int seed) {
		dungeonSeed = seed;
		levels = new List<Level>();
	}

	public void AddLevel(Level level) {
		levels.Add(level);
	}

	public Level GetLevel(int id) { 
		return levels[id];
	}
}

public class Level {
	public readonly int levelSeed;
	Room[,] rooms;

	public Level(int seed, int width, int height) {
		levelSeed = seed;
		rooms = new Room[width, height];
	}

	public void AddRoom(Room room, int x, int y) {
		rooms[x, y] = room;
	}

	public Room GetRoom(int x, int y) {
		return rooms[x, y];
	}

}

public class Room {
	public readonly int roomSeed;
	public readonly GameObject roomPrefab;
	public readonly RoomPrefab roomType;

	public Room(int seed, GameObject prefab, RoomPrefab type) {
		roomSeed = seed;
		roomPrefab = prefab;
		roomType = type;
	}

	public void DebugDraw(int x, int y, float roomOffset, float roomWidth, float roomHeight) {
		float t =  y * roomHeight + (y + 1) * roomOffset;
		float b =  (y + 1) * roomHeight + (y + 1) * roomOffset;
		float l =  x * roomWidth + (x + 1) * roomOffset;
		float r =  (x + 1) * roomWidth + (x + 1) * roomOffset;
		Debug.DrawLine(new Vector3(l, t, 0), new Vector3(r, t, 0), Color.green);
		Debug.DrawLine(new Vector3(l, t, 0), new Vector3(l, b, 0), Color.green);
		Debug.DrawLine(new Vector3(r, t, 0), new Vector3(r, b, 0), Color.green);
		Debug.DrawLine(new Vector3(l, b, 0), new Vector3(r, b, 0), Color.green);

		float centerX = (l + r) / 2;
		float centerY = (t + b) / 2;

		if(roomType.connectsNorth) {
			Debug.DrawLine(new Vector3(centerX, centerY, 0), new Vector3(centerX, centerY - ((roomHeight / 2) + roomOffset / 2), 0), Color.red);
		}
		if(roomType.connectsWest) {
			Debug.DrawLine(new Vector3(centerX, centerY, 0), new Vector3(centerX - ((roomWidth / 2) + roomOffset / 2), centerY, 0), Color.red);
		}
		if(roomType.connectsEast) {
			Debug.DrawLine(new Vector3(centerX, centerY, 0), new Vector3(centerX + ((roomWidth / 2) + roomOffset / 2), centerY , 0), Color.red);
		}
		if(roomType.connectsSouth) {
			Debug.DrawLine(new Vector3(centerX, centerY, 0), new Vector3(centerX, centerY + ((roomHeight / 2) + roomOffset / 2), 0), Color.red);
		}
	} 
}