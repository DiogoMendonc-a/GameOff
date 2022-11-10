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
	public List<RoomPrefab> rooms;

	public Level(int seed) {
		levelSeed = seed;
		rooms = new List<RoomPrefab>();
	}

	public void Degenerate() {
		rooms = new List<RoomPrefab>();
	}

	public void AddRoom(RoomPrefab room) {
		rooms.Add(room);
	}
}