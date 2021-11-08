using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRequirements : MonoBehaviour
{
    [SerializeField]
    private DisplayData craftRequirement;

    [SerializeField]
    private CraftingSlot craftData;

    public void GenerateRecipeData() {
        for (int i = 0; i < craftData.craft.craftingRecipe.recipeItems.Length; i++)
        {
            craftRequirement = Instantiate(craftRequirement, this.transform, true);
            craftRequirement.ingredientImg.sprite = craftData.craft.craftingRecipe.recipeItems[i].item.preview;
            craftRequirement.ingredientAmount.text = craftData.craft.craftingRecipe.recipeItems[i].amount.ToString();
            craftRequirement.separator.text = "X";
            craftRequirement.ingredientName.text = craftData.craft.craftingRecipe.recipeItems[i].item.itemName;
        }
    }

    public void ClearData() {
        craftRequirement.ingredientAmount.text = null;
        craftRequirement.separator.text = null;
        craftRequirement.ingredientImg.sprite = null;
        craftRequirement.ingredientName.text = null;
}
}
