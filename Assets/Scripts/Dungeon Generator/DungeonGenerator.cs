using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour{
	public float roomWidth;
	public float roomHeight;
	public float roomOffset;

	public int numberOfLevels;
	public int levelWidth;
	public int levelHeight;

	public GameObject[] prefabs;
	GameObject[] southConnectingPrefabs;
	GameObject[] eastConnectingPrefabs;
	GameObject[] westConnectingPrefabs;
	GameObject[] northConnectingPrefabs;
	Dungeon d; //DEBUG
	void Start() {
		PreClassifyRooms();
		DebugGenerate(); //DEBUG
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.G)) { //DEBUG
			DebugGenerate();
		}	
		DebugDrawLevel(d.GetLevel(0));
	}

	//DEBUG
	void DebugGenerate() {
		d = GenerateDungeon(new System.Random().Next());
	}

	void PreClassifyRooms() {
		List<GameObject> sRooms = new List<GameObject>();
		List<GameObject> eRooms = new List<GameObject>();
		List<GameObject> wRooms = new List<GameObject>();
		List<GameObject> nRooms = new List<GameObject>();

		foreach (GameObject room in prefabs)
		{
			RoomPrefab type = room.GetComponent<RoomPrefab>();
			if(type.connectsSouth) sRooms.Add(room);
			if(type.connectsEast) eRooms.Add(room);
			if(type.connectsWest) wRooms.Add(room);
			if(type.connectsNorth) nRooms.Add(room);
		}

		southConnectingPrefabs = sRooms.ToArray();
		eastConnectingPrefabs = eRooms.ToArray();
		westConnectingPrefabs = wRooms.ToArray();
		northConnectingPrefabs = nRooms.ToArray();
	}

	public Dungeon GenerateDungeon(int seed) {
		Dungeon dungeon = new Dungeon(seed);
		System.Random rng = new System.Random(seed);
		for (int i = 0; i < numberOfLevels; i++)
		{
			int levelSeed = rng.Next();
			dungeon.AddLevel(GenerateLevel(levelSeed));
		}
		return dungeon;
	}

	public Level GenerateLevel(int seed) {
		Level level = new Level(seed, levelWidth, levelHeight);
		System.Random rng = new System.Random(seed);
		int x = rng.Next() % levelWidth;
		int y = rng.Next() % levelHeight;
		GenerateRoom(rng.Next(), level, x, y, prefabs);
		return level;
	}

	public GameObject[] GetPossibleRooms(GameObject[] pool, bool north, bool west, bool east, bool south) {
		List<GameObject> validRooms = new List<GameObject>();
		foreach (GameObject room in pool)
		{
			RoomPrefab type = room.GetComponent<RoomPrefab>();
			if(type.connectsNorth && !north) continue;
			if(type.connectsWest && !west) continue;
			if(type.connectsEast && !east) continue;
			if(type.connectsSouth && !south) continue;
			validRooms.Add(room);
		}
		return validRooms.ToArray();
	}

	public void GenerateRoom(int seed, Level level, int x, int y, GameObject[] possibleRooms,string debug_offset = "") {
		System.Random rng = new System.Random(seed);
		bool canGoNorth = false;
		bool canGoWest = false;
		bool canGoEast = false;
		bool canGoSouth = false;

		if(y > 0) {
			Room neighbour = level.GetRoom(x, y - 1);
			if(neighbour != null) {
				if(neighbour.roomType.connectsSouth) {
					canGoNorth = true;
				}
			}
			else {
				canGoNorth = true;
			}
		}

		if(x > 0) {
			Room neighbour = level.GetRoom(x - 1, y);
			if(neighbour != null) {
				if(neighbour.roomType.connectsEast) {
					canGoWest = true;
				}
			}
			else {
				canGoWest = true;
			}
		}

		if(x < levelWidth - 1) {
			Room neighbour = level.GetRoom(x + 1, y);
			if(neighbour != null) {
				if(neighbour.roomType.connectsWest) {
					canGoEast = true;
				}
			}
			else {
				canGoEast = true;
			}
		}

		if(y < levelHeight - 1) {
			Room neighbour = level.GetRoom(x, y + 1);
			if(neighbour != null) {
				if(neighbour.roomType.connectsNorth) {
					canGoSouth = true;
				}
			}
			else {
				canGoSouth = true;
			}
		}

		GameObject[] validPrefabs = GetPossibleRooms(possibleRooms, canGoNorth, canGoWest, canGoEast, canGoSouth);
		GameObject prefab = validPrefabs[rng.Next()%validPrefabs.Length];
		RoomPrefab type = prefab.GetComponent<RoomPrefab>();
		Room room = new Room(seed, prefab, type);
		level.AddRoom(room, x, y);
		if (type.connectsNorth && level.GetRoom(x, y - 1) == null) {
			GenerateRoom(rng.Next(), level, x, y - 1, southConnectingPrefabs, debug_offset += "-");
		}
		if (type.connectsWest && level.GetRoom(x - 1, y) == null) {
			GenerateRoom(rng.Next(), level, x - 1, y, eastConnectingPrefabs, debug_offset += "-");
		}
		if (type.connectsEast && level.GetRoom(x + 1, y) == null) {
			GenerateRoom(rng.Next(), level, x + 1, y, westConnectingPrefabs, debug_offset += "-");
		}
		if (type.connectsSouth && level.GetRoom(x, y + 1) == null) {
			GenerateRoom(rng.Next(), level, x, y + 1, northConnectingPrefabs, debug_offset += "-");
		}
	}

	//DEBUG
	public void DebugDrawLevel(Level level) {
		float w = roomWidth * levelWidth + (levelWidth + 1) * roomOffset;
		float h = roomHeight * levelHeight + (levelHeight + 1) * roomOffset;
		Debug.DrawLine(new Vector3(0, h, 0), new Vector3(w, h, 0), Color.black);
		Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(w, 0, 0), Color.black);
		Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(0, h, 0), Color.black);
		Debug.DrawLine(new Vector3(w, 0, 0), new Vector3(w, h, 0), Color.black);
		
		for (int i = 0; i < levelWidth; i++)
		{
			for (int j = 0; j < levelHeight; j++)
			{
				Room room = level.GetRoom(i, j);
				if(room != null) room.DebugDraw(i, j, roomOffset, roomWidth, roomHeight);
			}
		}
	}
}