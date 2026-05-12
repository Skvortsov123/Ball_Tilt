using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static int[] worldLevelCounts = {
    7, // Tutorial has 7 levels
    2, // DSV has 2 levels
    2, // Golf has 2 levels
    5, // Kitchen has 5 levels
    5, // Mirror has 5 levels
    
    };

    //Kör i slutet av sessionen för att spara och ladda inställningar
    public static void saveSettings()
    {
        PlayerPrefs.SetFloat("Sensitivity", GameSettings.sensitivity);
        PlayerPrefs.SetFloat("DeadZone", GameSettings.deadZone);
        PlayerPrefs.SetFloat("MusicVolume", GameSettings.musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", GameSettings.sfxVolume);
        PlayerPrefs.SetInt("MusicMuted", GameSettings.musicMuted ? 1 : 0);
        PlayerPrefs.SetInt("ControlMode", (int)GameSettings.controlMode);
        PlayerPrefs.SetInt("JoystickMode", (int)GameSettings.joystickMode);
        PlayerPrefs.SetFloat("CalibrationOffsetX", GameSettings.calibrationOffset.x);
        PlayerPrefs.SetFloat("CalibrationOffsetY", GameSettings.calibrationOffset.y);
        PlayerPrefs.SetFloat("CalibrationOffsetZ", GameSettings.calibrationOffset.z);
        PlayerPrefs.Save();
    }

    //Kör i början av sessionen för att spara och ladda inställningar
    public static void loadSettings()
    {
        GameSettings.sensitivity = PlayerPrefs.GetFloat("Sensitivity", 2.5f);
        GameSettings.deadZone = PlayerPrefs.GetFloat("DeadZone", 0.05f);
        GameSettings.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        GameSettings.sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 3f);
        GameSettings.musicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        GameSettings.controlMode = (ControlMode)PlayerPrefs.GetInt("ControlMode", (int)ControlMode.Tilt);
        GameSettings.joystickMode = (JoystickMode)PlayerPrefs.GetInt("JoystickMode", (int)JoystickMode.Left);
        float offsetX = PlayerPrefs.GetFloat("CalibrationOffsetX", 0f);
        float offsetY = PlayerPrefs.GetFloat("CalibrationOffsetY", 0f);
        float offsetZ = PlayerPrefs.GetFloat("CalibrationOffsetZ", 0f);
        GameSettings.calibrationOffset = new Vector3(offsetX, offsetY, offsetZ);
    }
    public static void completeLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("levelCompleted_" + levelIndex, 1);
        PlayerPrefs.Save();
    }
    public static void unlockWorld(int worldIndex)
    {
        PlayerPrefs.SetInt("worldsUnlocked" + worldIndex, 1);
        PlayerPrefs.Save();
    }

    public static bool IsWorldUnlocked(int worldIndex)
    {
        if (worldIndex == 1)
            return true; // första världen alltid upplåst

        return PlayerPrefs.GetInt("worldsUnlocked" + worldIndex, 0) == 1;
    }

    public static void checkWorldUnlock(int worldIndex)
    {
        int completionPercentage = getWorldCompletionPercentage(worldIndex);
        if (completionPercentage >= 100)
        {
            unlockWorld(worldIndex + 1);
        }
    }

    public static int GetGlobalLevelIndex(int worldIndex, int localLevelIndex)
    {
        int globalIndex = localLevelIndex;

        for (int i = 0; i < worldIndex - 1; i++)
        {
            globalIndex += worldLevelCounts[i];
        }

        return globalIndex;
    }

    public static int getWorldCompletionPercentage(int worldIndex)
    {

        int totalLevels = worldLevelCounts[worldIndex - 1];
        int startLevel = 1;

        for (int i = 0; i < worldIndex - 1; i++)
        {
            startLevel += worldLevelCounts[i];
        }
        int completedLevels = 0;
        for(int i = startLevel; i < startLevel + totalLevels; i++)
        {
            if(PlayerPrefs.GetInt("levelCompleted_" + i, 0) == 1)
            {
                completedLevels++;
            }
        }

        return Mathf.RoundToInt((float)completedLevels / totalLevels * 100); //procent av världens nivåer som är klara
    }


}