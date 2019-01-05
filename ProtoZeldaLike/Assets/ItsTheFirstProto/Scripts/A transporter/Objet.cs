using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Inventory/Item")]
public class Objet : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    [TextArea(3,10)]
    public string descripttion;
    public float attack;
    public float defense;
    public float agility;
    public float life;

    public float price;
    public int type; //0: Tête 1: Plastron 2: Ceinture 3: Bas 4: Boots 5: Arme 

}
