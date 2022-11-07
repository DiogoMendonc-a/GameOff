using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour{
	public int numberOfLevels;

	public float entranceDeadzone = 0.001f;
	public int maxDepth;

	public int maxRoomSpawnTries;
	public GameObject[] prefabs;
	public GameObject[] startingRooms;
	public List<GameObject> onceRooms;

	[Range(0,100)]
	public int onceRoomChance;
	List<GameObject> onceRoomsRemaining;
	
	public GameObject[] bossRooms;
	List<GameObject> bossRoomsRemaining;

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
			Destroy(transform.GetChild(i).gameObject);
		}
	}	

	void Update() {
		if(Input.GetKeyDown(KeyCode.G)) { //DEBUG
			KillAllChildren();
			DebugGenerate();
		}	
	}

	//DEBUG
	void DebugGenerate() {
		d = GenerateDungeon(new System.Random().Next());
		GenerateLevel(0);
	}

	public Dungeon GenerateDungeon(int seed) {
		Dungeon dungeon = new Dungeon(seed);
		System.Random rng = new System.Random(seed);

		//initialize lists
		onceRoomsRemaining = new List<GameObject>(onceRooms);
		bossRoomsRemaining = new List<GameObject>(bossRooms);

		
		for (int i = 0; i < numberOfLevels; i++)
		{
			int levelSeed = rng.Next();
			dungeon.AddLevel(new Level(seed));
		}
		return dungeon;
	}

	Level GenerateLevel(int levelId) {
		Level level = d.GetLevel(levelId);
		System.Random rng = new System.Random(level.levelSeed);

		boundingBoxes = new List<Rect>();
		
		GameObject startingRoom = startingRooms[rng.Next() % startingRooms.Length];
		
		GenerateRoom(rng.Next(), Vector3.zero, Quaternion.identity, level, startingRoom, 0);
		GenerateBossRoom(level);
		
		foreach(RoomPrefab room in level.rooms) {
			CheckConnections(room, level);
		}
		return level;
	}

	void CheckConnections(RoomPrefab room, Level level) {
		foreach(RoomPrefab other in level.rooms) {
			if(other == room) continue;
			foreach (GameObject exit in room.entrances)
			{
				if(!exit.activeSelf) continue;
				foreach (GameObject entrance in other.entrances)
				{
					if(!entrance.activeSelf) continue;
					if((exit.transform.position - entrance.transform.position).sqrMagnitude < entranceDeadzone) {
						CreateConnection(exit, entrance);
					}
				}
			}
		}
	}

	void CreateConnection(GameObject exit, GameObject entrance) {
		exit.GetComponent<SpriteRenderer>().color = Color.blue;
		entrance.GetComponent<SpriteRenderer>().color = Color.blue;

//		exit.SetActive(false);
//		entrance.SetActive(false);
	}

	bool ValidPlacement(Vector3 displacement, Quaternion rotation, RoomPrefab type) {
		Rect[] bbs = type.GetBoundingBoxes(displacement, rotation);
		foreach (Rect r in boundingBoxes)
		{
			foreach (Rect bb in bbs)
			{
				if(r.Overlaps(bb)) return false;
			}
		}
		return true;
	}

	GameObject GetRoom(System.Random rng) {
		int value = rng.Next()%100;
		if(value < onceRoomChance && onceRoomsRemaining.Count > 0) {
			return onceRoomsRemaining[rng.Next()%onceRoomsRemaining.Count];
		}
		return prefabs[rng.Next() % prefabs.Length];
	}

	void GenerateAtExit(System.Random rng, Level level, GameObject exit, int depth) {
		for(int i = 0; i < maxRoomSpawnTries; i++) {
			GameObject possibleNeighbour = GetRoom(rng);
			RoomPrefab neighbourType = possibleNeighbour.GetComponent<RoomPrefab>();
			foreach (Quaternion possibleRotation in rotations)
			{
				foreach (GameObject entrance in neighbourType.entrances)
				{
					Vector3 displacement = possibleRotation * -entrance.transform.position;
					displacement += exit.transform.position;
					if(ValidPlacement(displacement, possibleRotation, neighbourType)) {
						GenerateRoom(rng.Next(), displacement, possibleRotation, level, possibleNeighbour, depth + 1);
						return;
					}
				}
			}
		}
	}

	void GenerateRoom(int seed, Vector3 position, Quaternion rotation, Level level, GameObject prefab, int depth) {
		System.Random rng = new System.Random(seed);
		//GameObject prefab = possibleRooms[rng.Next() % possibleRooms.Length];
		GameObject newRoom = GameObject.Instantiate(prefab, position, rotation);
		RoomPrefab type = newRoom.GetComponent<RoomPrefab>();
		level.AddRoom(type);
		newRoom.transform.parent = this.transform;
		boundingBoxes.AddRange(type.GetBoundingBoxes(position, rotation));
		if(onceRoomsRemaining.Contains(prefab)) onceRoomsRemaining.Remove(prefab);
		if(depth == maxDepth) return;

		foreach (GameObject exit in type.entrances)
		{
			GenerateAtExit(rng, level, exit, depth);
		}
	}

	void GenerateBossRoom(Level level) {
		System.Random rng = new System.Random(level.levelSeed);
		if(bossRoomsRemaining.Count == 0) {
			Debug.LogWarning("No Boss Rooms Remain to Be Placed! Reseting List");
			bossRoomsRemaining = new List<GameObject>(bossRooms);
		}
		GameObject bossRoom = bossRoomsRemaining[rng.Next()%bossRoomsRemaining.Count];
		RoomPrefab bossRoomType = bossRoom.GetComponent<RoomPrefab>();
		foreach(GameObject entrance in bossRoomType.entrances) {
			foreach (RoomPrefab room in level.rooms)
			{
				foreach (GameObject exit in room.entrances)
				{
					foreach (Quaternion possibleRotation in rotations) {
						Vector3 displacement = possibleRotation * -entrance.transform.position;
						displacement += exit.transform.position;
						if(ValidPlacement(displacement, possibleRotation, bossRoomType)) {
							GameObject newRoom = GameObject.Instantiate(bossRoom, displacement, possibleRotation);
							RoomPrefab type = newRoom.GetComponent<RoomPrefab>();
							level.AddRoom(type);
							newRoom.transform.parent = this.transform;
							boundingBoxes.AddRange(type.GetBoundingBoxes(displacement, possibleRotation));
							bossRoomsRemaining.Remove(bossRoom);
							return;
						}
					}
				}
			}
		}
	}
}