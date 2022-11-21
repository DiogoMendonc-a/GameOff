public class LevelProgresser : Interactable {
	public override void Activate() {
       	GameManager.instance.LoadLevel(GameManager.instance.currentLevel + 1);
    }
}