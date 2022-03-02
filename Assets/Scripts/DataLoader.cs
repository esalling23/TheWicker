using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLoader
{
  #region Classes

  [System.Serializable]
  public class JsonIngredients
  {
      // matches json file "ingredients" key
      public Ingredient[] ingredients;
  }

  #endregion

  #region Methods

  /// <summary>
  /// Constructor
  /// </summary>
  public DataLoader() {}

  /// <summary>
  /// Load JSON file path of Ingredient objects
  /// </summary>
  /// <param name="path"></param>
  /// <returns></returns>
  public Ingredient[] LoadIngredients(string path)
  {
      string jsonString = File.ReadAllText(path);
      JsonIngredients data = JsonUtility.FromJson<JsonIngredients>(jsonString);

      return data.ingredients;
  }
  
  #endregion
}
