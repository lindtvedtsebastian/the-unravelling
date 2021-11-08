using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRequirements : MonoBehaviour
{
    [SerializeField]
    private DisplayData craftRequirement;

    [SerializeField]
    private CraftingSlot craftData;

    void Start() {
    }

    public void GenerateRecipeData() {
        for (int i = 0; i < craftData.craft.craftingRecipe.recipeItems.Length; i++)
        {
            craftRequirement = Instantiate(craftRequirement, this.transform, true);
            craftRequirement.ingredientImg.sprite = craftData.craft.craftingRecipe.recipeItems[i].item.preview;
            craftRequirement.ingredientAmount.text = craftData.craft.craftingRecipe.recipeItems[i].amount.ToString();
            craftRequirement.ingredientName.text = craftData.craft.craftingRecipe.recipeItems[i].item.itemName;
        }
    }
}
