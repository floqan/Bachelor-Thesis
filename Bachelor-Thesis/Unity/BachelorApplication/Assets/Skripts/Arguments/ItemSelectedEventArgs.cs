using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectSelectedEventArgs
{
    public GameObject Selection { get; set; }
    public List<SceneObjectParameterModel> Parameter { get; set; }
}
