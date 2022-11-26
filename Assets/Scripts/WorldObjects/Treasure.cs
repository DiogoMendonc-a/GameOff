public class Treasure : Interactable, IGeneratable
{
    Obtainable item;

    void IGeneratable.Generate(int seed) {
        System.Random rng = new System.Random(seed);
		item = LootManager.instance.GetLoot(rng.Next());
    }

    public override void Activate() {
       	InGameUIManager.instance.ActivateTreasureUI(item, HandleResponse);
    }
    
	void HandleResponse(bool answer) {
		if(answer) {
			PlayerClass.instance.inventory.AddObtainable(item);
			interactionEnabled = false;
			Destroy(gameObject);		
		}
	}
}
