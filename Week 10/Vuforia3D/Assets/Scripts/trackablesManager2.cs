using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class trackablesManager2 : MonoBehaviour {

    private bool astroTracked, moonTracked;
    private Vector3 astroPos, moonPos;

    // Start is called before the first frame update
    void Start() {
  
    }

    // Update is called once per frame
    void Update() {

        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> allTrackables = sm.GetActiveTrackableBehaviours();


        foreach (TrackableBehaviour tb in allTrackables) {


             if (tb.Trackable.Name == "astronaut") {
                if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
                    astroTracked = true;
                    astroPos = tb.transform.position;
            
                } else {
                    astroTracked = false;
                }
            }
             
            if (tb.Trackable.Name == "moon") {
                if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
                    moonTracked = true;
                    moonPos = tb.transform.position;
            
                } else {
                    moonTracked = false;
                }
            }

            if (astroTracked == true && moonTracked == true) {
                Debug.Log("GOTEM");
            } else {
                // line.enabled = false;
            }
        }

    }

}
