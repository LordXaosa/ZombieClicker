using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SavedData  {

	public int CurrentLevel = 1;
	public decimal Money;
	public decimal TotalClicks;
	public decimal TotalKills;

	public static SavedData Data;
	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGame.sav"); //you can call it anything you want
		bf.Serialize(file, Data);
		file.Close();
	}

	public static bool Load()
	{
		string filePath = Application.persistentDataPath + "/savedGame.sav";
		if(File.Exists(filePath)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGame.sav", FileMode.Open);
			SavedData.Data = (SavedData)bf.Deserialize(file);
			file.Close();
			return true;
		}
		return false;
	}
}
