using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSceneData", menuName = "Custom/SceneData/NewSceneData")]
public class DataScene : ScriptableObject
{
    public SceneReference scene;
}
