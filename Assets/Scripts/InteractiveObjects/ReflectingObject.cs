using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectingObject : MonoBehaviour
{

    public enum ReflectionType { full, rightAngle }

    [SerializeField]
    private ReflectionType _reflectionType;

    public ReflectionType reflectionType
    {
        get { return _reflectionType; }
    }
}
