using UnityEngine.UI;
using UnityEngine;

public class IngredientImage : MonoBehaviour
{
    #region Fields

    private Image _ingredientImage;

    #endregion

    #region Properties
    
    #endregion

    #region Methods

    private void Start() {
        
    }
    
    public void Init(IngredientName ingredient) 
    {
        _ingredientImage = GetComponent<Image>();
        _ingredientImage.sprite = GameManager.Instance.AllIngredients[ingredient].image;
    }

    #endregion
}
