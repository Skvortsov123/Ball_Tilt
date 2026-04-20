using UnityEngine;

public enum ControlMode
{
    Tilt,
    Joystick,
    Slider
}

public static class GameSettings
{
    public static float sensitivity = 1f;
    public static float deadZone = 0.05f;
    public static float musicVolume = 1f;
    public static float sfxVolume = 1f;


    public static ControlMode controlMode = ControlMode.Tilt; //  nytt system (3 lägen)

    public static Vector3 calibrationOffset = Vector3.zero;
}