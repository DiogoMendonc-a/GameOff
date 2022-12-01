using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	DungeonGenerator generator;
	Dungeon dungeon;

	public int currentLevel { get; private set; }
	public int currentMasterSeed;

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
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if(scene.name == "GameScene") {
			generator.GenerateLevel(dungeon, currentLevel);
			//Start music here
		}
	}

	public void GenerateNewDungeon(int seed) {
		currentMasterSeed = seed;
		dungeon = generator.GenerateDungeon(seed);
	}

	public void GenerateNewDungeon() {
		GenerateNewDungeon(new System.Random().Next());
	}

	public void StartNewGame(int seed) {
		GenerateNewDungeon(seed);
		currentLevel = 0;
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
	}

	public void ReturnToMenu() {
		if(currentLevel > -1) generator.DegenerateLevel(dungeon.GetLevel(currentLevel));
		//Start music here
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

	public void LoadLevel(int levelId) {
		if(currentLevel > -1) generator.DegenerateLevel(dungeon.GetLevel(currentLevel));
		if(levelId >= generator.numberOfLevels) {
			WinGame();
			currentLevel = -1;
			return;
		}
		QuickBuck.triggered = false;
		currentLevel = levelId;
		PlayerClass.instance.transform.position = new Vector3();
		generator.GenerateLevel(dungeon, currentLevel);
	}

	public void LoseGame() {
		InGameUIManager.instance.ActivateDeathUI();
	}

	public void WinGame() {
		InGameUIManager.instance.ActivateVictoryUI();
	}
}