using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeTable", menuName = "ScriptableObjects/RecipeTable", order = 3)]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField]
    public Item componentOne;

    [SerializeField]
    public Item componentTwo;

    [SerializeField]
    public Item result;

}
