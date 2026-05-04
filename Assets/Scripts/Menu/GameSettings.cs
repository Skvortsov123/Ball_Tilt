using UnityEngine;

public enum ControlMode
{
    Tilt,
    Joystick,
    Slider
}

public enum JoystickMode
{
    Left,
    Right,
    Touch
}


public static class GameSettings
{
    public static float sensitivity = 2.5f;
    public static float deadZone = 0.05f;
    public static float musicVolume = 0.5f;
    public static float sfxVolume = 3f;
    public static bool musicMuted = false;

    public static ControlMode controlMode = ControlMode.Tilt; 
    public static JoystickMode joystickMode = JoystickMode.Left;

    public static Vector3 calibrationOffset;
}