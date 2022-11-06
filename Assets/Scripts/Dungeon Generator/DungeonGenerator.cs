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
	public int maxDepth;

	public int maxRoomSpawnTries;
	public GameObject[] prefabs;
	public GameObject[] startingRooms;
	Dungeon d; //DEBUG
	
	List<Rect> boundingBoxes;

	Quaternion[] rotations;

	void Awake() {
		rotations = new Quaternion[] {
			Quaternion.Euler(0, 0, 0),
			Quaternion.Euler(0, 0, 90),
			Quaternion.Euler(0, 0, 180),
			Quaternion.Euler(0, 0, -90),
		};
	}

	void Start() {
		
		DebugGenerate(); //DEBUG
	}

	void KillAllChildren() {
		List<Transform> children = new List<Transform>();
		for(int i = 0; i < transform.childCount; i++) {
			children.Add(transform.GetChild(i));
		}
		foreach (Transform child in children)
		{
			Destroy(child.gameObject);
		}
	}	

	void Update() {
		if(Input.GetKeyDown(KeyCode.G)) { //DEBUG
			KillAllChildren();
			DebugGenerate();
		}	

		DebugDrawLevel(d.GetLevel(0));
	}

	//DEBUG
	void DebugGenerate() {
		d = GenerateDungeon(new System.Random().Next());
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
		boundingBoxes = new List<Rect>();
		int x = rng.Next() % levelWidth;
		int y = rng.Next() % levelHeight;
		GameObject startingRoom = startingRooms[rng.Next() % startingRooms.Length];
		GenerateRoom(rng.Next(), Vector3.zero, Quaternion.identity, level, startingRoom, 0);
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

	public bool ValidPlacement(Vector3 displacement, Quaternion rotation, RoomPrefab type) {
		Rect bb = type.GetBoundingBox(displacement, rotation);
		foreach (Rect r in boundingBoxes)
		{
			if(r.Overlaps(bb)) return false;
		}
		return true;
	}

	void GenerateAtExit(System.Random rng, Level level, GameObject exit, int depth) {
		Debug.Log("Generating At Exit At " + exit.transform.position + " Depth: " + depth);
		for(int i = 0; i < maxRoomSpawnTries; i++) {
			GameObject possibleNeighbour = prefabs[rng.Next() % prefabs.Length];
			RoomPrefab neighbourType = possibleNeighbour.GetComponent<RoomPrefab>();
			foreach (Quaternion possibleRotation in rotations)
			{
				foreach (GameObject entrance in neighbourType.entrances)
				{
					Vector3 displacement = possibleRotation * -entrance.transform.position;
					displacement += exit.transform.position;
					if(ValidPlacement(displacement, possibleRotation, neighbourType)) {
						Debug.Log("		Generating Room " + possibleRotation.eulerAngles + " : " + displacement + " " + neighbourType.GetBoundingBox(displacement, possibleRotation));
						GenerateRoom(rng.Next(), displacement, possibleRotation, level, possibleNeighbour, depth + 1);
						return;
					}
				}
			}
		}
	}

	public void GenerateRoom(int seed, Vector3 position, Quaternion rotation, Level level, GameObject prefab, int depth) {
		System.Random rng = new System.Random(seed);
		//GameObject prefab = possibleRooms[rng.Next() % possibleRooms.Length];
		GameObject newRoom = GameObject.Instantiate(prefab, position, rotation);
		RoomPrefab type = newRoom.GetComponent<RoomPrefab>();
		newRoom.transform.parent = this.transform;
		boundingBoxes.Add(type.GetBoundingBox(position, rotation));
		if(depth == maxDepth) return;

		foreach (GameObject exit in type.entrances)
		{
			GenerateAtExit(rng, level, exit, depth);
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