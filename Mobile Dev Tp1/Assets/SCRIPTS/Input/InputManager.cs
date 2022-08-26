using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using System.Linq;

public class InputManager
{
    static InputManager instance = null;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputManager();
            }

            return instance;
        }
    }

    Dictionary<string, float> axisValues = new Dictionary<string, float>();


    public void SetAxis(string axis, float value)
    {
        if (!axisValues.ContainsKey(axis))
            axisValues.Add(axis, value);
        axisValues[axis] = value;
    }

    float GetOrAddAxis(string axis)
    {
        if (!axisValues.ContainsKey(axis))
            axisValues.Add(axis, 0f);
        return axisValues[axis];
    }

    public float GetAxis(string axis)
    {
#if UNITY_EDITOR
        return GetOrAddAxis(axis) + Input.GetAxis(axis);
#elif UNITY_ANDROID || UNITY_IOS
        return GetOrAddAxis(axis);
#elif UNITY_STANDALONE
        return Input.GetAxis(axis);
#endif
    }

    public bool GetButton(string button)
    {
        return Input.GetButton(button);
    }
}