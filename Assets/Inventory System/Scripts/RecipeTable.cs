using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeTable", menuName = "ScriptableObjects/RecipeTable", order = 4)]
public class RecipeTable : ScriptableObject
{
    // to contain result of recipes
    [SerializeField]
    private Recipes[] recipeResults;
}
