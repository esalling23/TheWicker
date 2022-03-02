/// <summary>
/// Settings object with different constants for gameplay
/// </summary>
[System.Serializable]
public struct LevelSettings
{
    /// <summary>
    /// The level timer duration
    /// </summary>
    public float levelTimeLimit;
    /// <summary>
    /// Max # of ingredients per order 
    /// </summary>
    public int orderIngredientLimit;
    /// <summary>
    /// Minimum order time
    /// </summary>
    public float minOrderTime;
    /// <summary>
    /// Maximum order time
    /// </summary>
    public float maxOrderTime;
}