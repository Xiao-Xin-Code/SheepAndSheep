using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/Game Config")]
public class GameConfig : ScriptableObject
{
	public bool MusicIsOn = true;
	public bool SfxIsOn = true;
	public bool ShakeIsOn = false;
	public string Theme = "Default";
}
