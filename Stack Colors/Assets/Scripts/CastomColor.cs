/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

// Actually the name of the class should`ve be CustomColor)
public class CastomColor : MonoBehaviour
{
    [SerializeField] private string color_name;
    [SerializeField] private Color color;
    public string Color_name { get { return color_name; } }
    public Color Color { get { return color; } }
}
