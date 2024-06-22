using UnityEngine;

[CreateAssetMenu(fileName = "SpriteData", menuName = "Custom/Sprite Data")]
public class SpriteDataSO : ScriptableObject
{
    public Sprite standardRenderer;
    public Sprite[] sprites ;
}