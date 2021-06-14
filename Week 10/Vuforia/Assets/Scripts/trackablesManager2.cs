using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class trackablesManager2 : MonoBehaviour {

    private bool bTarget1, bTarget2;
    private Vector3 vT1Pos, vT2Pos;

    public AudioSource audio; 
    public GameObject snitch; 

    // Start is called before the first frame update
    void Start() {
  
    }

    // Update is called once per frame
    void Update() {

        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> allTrackables = sm.GetActiveTrackableBehaviours();


        foreach (TrackableBehaviour tb in allTrackables) {

            // Debug.Log("tracking -> " + tb.Trackable.Name);

            if (tb.Trackable.Name == "harrypotter") {
                if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
                    bTarget1 = true;
                    vT1Pos = tb.transform.position;
                    if (!audio.isPlaying) {
                        audio.Play();
                    }
                } else {
                    bTarget1 = false;
                    if (audio.isPlaying) {
                        audio.Pause();
                    }
                }
            }

            if (bTarget1 == true && bTarget2 == true) {
                
            } else {
                // line.enabled = false;
            }
        }

    }

}
