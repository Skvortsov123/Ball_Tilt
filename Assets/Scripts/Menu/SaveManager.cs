using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Antal levels i varje world
    // Index 0 = World 1, Index 1 = World 2 osv.
    public static int[] worldLevelCounts = {
        7, // Tutorial har 7 levels
        3,  // Illusion har 3 levels
        2, // Golf har 2 levels
        2, // DSV har 2 levels
        5 // Kitchen har 5 levels
        
    };

    // Sparar alla spelarens settings till PlayerPrefs
    // Körs när spelaren ändrar inställningar eller lämnar spelet
    public static void saveSettings()
    {
        // Sensitivity / Deadzone
        PlayerPrefs.SetFloat("Sensitivity", GameSettings.sensitivity);
        PlayerPrefs.SetFloat("DeadZone", GameSettings.deadZone);

        // Ljudinställningar
        PlayerPrefs.SetFloat("MusicVolume", GameSettings.musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", GameSettings.sfxVolume);
        PlayerPrefs.SetInt("MusicMuted", GameSettings.musicMuted ? 1 : 0);

        // Control mode (Tilt / Joystick / Slider)
        PlayerPrefs.SetInt("ControlMode", (int)GameSettings.controlMode);

        // Joystick mode (Left / Right / Touch)
        PlayerPrefs.SetInt("JoystickMode", (int)GameSettings.joystickMode);

        /*// Sparar calibration offset för tilt controls
        PlayerPrefs.SetFloat("CalibrationOffsetX", GameSettings.calibrationOffset.x);
        PlayerPrefs.SetFloat("CalibrationOffsetY", GameSettings.calibrationOffset.y);
        PlayerPrefs.SetFloat("CalibrationOffsetZ", GameSettings.calibrationOffset.z);
        */
        // Viktigt: faktiskt skriva till disk
        PlayerPrefs.Save();
        Debug.Log("Settings sparade!");
    }

    // Laddar alla sparade settings från PlayerPrefs
    // Körs när spelet startar
    public static void loadSettings()
    {
        // Om inget sparat finns används defaultvärden

        GameSettings.sensitivity =
            PlayerPrefs.GetFloat("Sensitivity", 2.5f);

        GameSettings.deadZone =
            PlayerPrefs.GetFloat("DeadZone", 0.05f);

        GameSettings.musicVolume =
            PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        GameSettings.sfxVolume =
            PlayerPrefs.GetFloat("SFXVolume", 3f);

        GameSettings.musicMuted =
            PlayerPrefs.GetInt("MusicMuted", 0) == 1;

        GameSettings.controlMode =
            (ControlMode)PlayerPrefs.GetInt(
                "ControlMode",
                (int)ControlMode.Tilt
            );

        GameSettings.joystickMode =
            (JoystickMode)PlayerPrefs.GetInt(
                "JoystickMode",
                (int)JoystickMode.Left
            );

        /*// Hämtar sparad calibration offset
        float offsetX =
            PlayerPrefs.GetFloat("CalibrationOffsetX", 0f);

        float offsetY =
            PlayerPrefs.GetFloat("CalibrationOffsetY", 0f);

        float offsetZ =
            PlayerPrefs.GetFloat("CalibrationOffsetZ", 0f);

        GameSettings.calibrationOffset =
            new Vector3(offsetX, offsetY, offsetZ);
        */
        Debug.Log("Settings loadade!");
    }

    // Markerar en level som klarad
    // Exempel: levelCompleted_8 = 1
    public static void completeLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("levelCompleted_" + levelIndex, 1);
        PlayerPrefs.Save();
    }

    // Låser upp en world
    // Exempel: worldsUnlocked2 = 1
    public static void unlockWorld(int worldIndex)
    {
        PlayerPrefs.SetInt("worldsUnlocked" + worldIndex, 1);
        PlayerPrefs.Save();
    }

    // Kollar om en world är upplåst
    public static bool IsWorldUnlocked(int worldIndex)
    {
        // Första världen är alltid upplåst
        if (worldIndex == 1)
            return true;

        // Returnerar true om worlden är sparad som upplåst
        return PlayerPrefs.GetInt(
            "worldsUnlocked" + worldIndex,
            0
        ) == 1;
    }

    // Kollar om hela världen är klar
    // Om ja, lås upp nästa world
    public static void checkWorldUnlock(int worldIndex)
    {
        int completionPercentage =
            getWorldCompletionPercentage(worldIndex);

        // Om världen är 100% klar
        if (completionPercentage >= 100)
        {
            // Lås upp nästa world
            unlockWorld(worldIndex + 1);
        }
    }

    // Konverterar local level index till globalt level index
    //
    // Exempel:
    // World 2, Level 1
    // blir levelCompleted_8
    public static int GetGlobalLevelIndex(int worldIndex, int localLevelIndex
)
    {
        int globalIndex = localLevelIndex;

        // Lägg till alla levels från tidigare worlds
        for (int i = 0; i < worldIndex - 1; i++)
        {
            globalIndex += worldLevelCounts[i];
        }

        return globalIndex;
    }

    // Räknar ut hur många procent av en world
    // som spelaren har klarat
    public static int getWorldCompletionPercentage(int worldIndex)
    {
        // Hur många levels finns i denna world?
        int totalLevels =
            worldLevelCounts[worldIndex - 1];

        // Hitta första global level för denna world
        int startLevel = 1;

        for (int i = 0; i < worldIndex - 1; i++)
        {
            startLevel += worldLevelCounts[i];
        }

        int completedLevels = 0;

        // Loopar igenom alla levels i världen
        for (int i = startLevel;
             i < startLevel + totalLevels;
             i++)
        {
            // Om level är klarad
            if (PlayerPrefs.GetInt(
                "levelCompleted_" + i,
                0
            ) == 1)
            {
                completedLevels++;
            }
        }

        // Returnerar procent färdigställande
        return Mathf.RoundToInt(
            (float)completedLevels / totalLevels * 100
        );
    }

    // Raderar alla sparade data (för debug och testning)
    public static void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("Alla PlayerPrefs har raderats.");
    }

    // raderar bara level- och world-progress, behåller settings
    public static void ResetLevelData()
    {
        for (int i = 1; i <= 50; i++)
        {
            PlayerPrefs.DeleteKey("levelCompleted_" + i);
        }

        for (int i = 1; i <= 10; i++)
        {
            PlayerPrefs.DeleteKey("worldsUnlocked" + i);
        }

        PlayerPrefs.Save();

        Debug.Log("Progress resetad.");
    }



}