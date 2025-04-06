using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class FileManager
{
	public static bool WriteToFile(string fileName, string fileContents)
	{
		var fullPath = Path.Combine(Application.persistentDataPath, fileName);

		try
		{
			File.WriteAllText(fullPath, fileContents);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to write to {fullPath} with exception {e}");
			return false;
		}
	}

	public static bool LoadFromFile(string fileName, out string result)
	{
		var fullPath = Path.Combine(Application.persistentDataPath, fileName);
		if(!File.Exists(fullPath))
		{
			File.WriteAllText(fullPath, ""); 
		}
		try
		{
			result = File.ReadAllText(fullPath);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to read from {fullPath} with exception {e}");
			result = "";
			return false;
		}
	}

	public static bool MoveFile(string fileName, string newFileName)
	{
		var fullPath = Path.Combine(Application.persistentDataPath, fileName);
		var newFullPath = Path.Combine(Application.persistentDataPath, newFileName);

		try
		{
			if (File.Exists(newFullPath))
			{
				File.Delete(newFullPath);
			}

			if (!File.Exists(fullPath))
			{
				return false;
			}
			
			File.Move(fullPath, newFullPath);
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to move file from {fullPath} to {newFullPath} with exception {e}");
			return false;
		}

		return true;
	}

	public static bool DeleteFile(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			Debug.LogError("File name is null or empty.");
			return false;
		}

		var fullPath = Path.Combine(Application.persistentDataPath, fileName);

		if (File.Exists(fullPath))
		{
			try
			{
				File.Delete(fullPath);
				return true;
			}
			catch (IOException e)
			{
				Debug.LogError($"Failed to delete file: {fullPath}. Error: {e.Message}");
			}
		}
		else
		{
			Debug.LogWarning($"File not found: {fullPath}");
		}

		return false;
	}
}
