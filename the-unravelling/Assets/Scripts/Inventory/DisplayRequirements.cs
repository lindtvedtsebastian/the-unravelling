using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRequirements : MonoBehaviour
{
    [SerializeField]
    private DisplayData craftRequirement;

    [SerializeField]
    private CraftingSlot craftData;

    public void GenerateRecipeData(Craft craft) {
        for (int i = 0; i < craft.craftingRecipe.recipeItems.Length; i++)
        {
            craftRequirement = Instantiate(craftRequirement, this.transform, true);
            craftRequirement.ingredientImg.sprite = craft.craftingRecipe.recipeItems[i].item.preview;
            craftRequirement.ingredientAmount.text = craft.craftingRecipe.recipeItems[i].amount.ToString();
            craftRequirement.separator.text = "X";
            craftRequirement.ingredientName.text = craft.craftingRecipe.recipeItems[i].item.itemName;
        }
    }

    public void ClearData() {
        craftRequirement.ingredientAmount.text = null;
        craftRequirement.separator.text = null;
        craftRequirement.ingredientImg.sprite = null;
        craftRequirement.ingredientName.text = null;
    }
}
