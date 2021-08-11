using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
   public SceneUtils.SceneId nextScene= SceneUtils.SceneId.Lobby;

   public void Go()
   {
      SceneLoader.Instance.LoadScene(SceneUtils.scenes[(int)nextScene]);  
   }
}
