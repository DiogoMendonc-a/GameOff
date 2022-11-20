public class Treasure : Interactable, IGeneratable
{
    bool taken = false;
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
			interactionEnabled = false;			
		}
		else {

		}
	}

}
