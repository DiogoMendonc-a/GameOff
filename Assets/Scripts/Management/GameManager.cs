using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	DungeonGenerator generator;
	Dungeon dungeon;

	public int currentLevel { get; private set; }

	private void Awake() {
		if(instance != null) {
			Debug.LogWarning("A GameManager is Already Present! Discarding New Instance.");
			Destroy(this.gameObject);
			return;
		}
		currentLevel = -1;
		DontDestroyOnLoad(this.gameObject);
		generator = GetComponent<DungeonGenerator>();
		instance = this;
	}

	public void GenerateNewDungeon(int seed) {
		dungeon = generator.GenerateDungeon(seed);
	}

	public void GenerateNewDungeon() {
		GenerateNewDungeon(new System.Random().Next());
	}

	public void StartNewGame() {
		GenerateNewDungeon();
		generator.GenerateLevel(dungeon, 0);
		currentLevel = 0;
	}

	public void LoadLevel(int levelId) {
		if(currentLevel > -1) generator.DegenerateLevel(dungeon.GetLevel(currentLevel));
		if(levelId >= generator.numberOfLevels) {
			WinGame();
			currentLevel = -1;
			return;
		}
		generator.GenerateLevel(dungeon, levelId);
		currentLevel = levelId;
	}

	public void LoseGame() {
		//TODO
	}

	public void WinGame() {
		Debug.Log("You Won The Game! Congrats");
		//TODO
	}
}