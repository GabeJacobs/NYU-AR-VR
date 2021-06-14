using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class trackablesManager2 : MonoBehaviour {

    private bool astroTracked, moonTracked;
    private Vector3 astroPos, moonPos;
    private LineRenderer line;
    public GameObject spaceShip;

    // Start is called before the first frame update
    void Start() {
        line = gameObject.AddComponent<LineRenderer>();
        line.widthMultiplier = .01f;
        line.positionCount = 2;
        line.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        line.startColor = Color.blue;
        line.endColor = Color.red;
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
                RaycastHit hit;
                Debug.DrawRay(astroPos, dir:(moonPos - astroPos), Color.green);
                //
                // if (Physics.Raycast(transform.position, tb.transform.right, out hit)) {
                //         Debug.DrawRay(astroPos, tb.transform.right, Color.green, hit.distance);
                // }
                

                spaceShip.transform.position += (moonPos - astroPos).normalized * (.1f * Time.deltaTime);
            } else if (astroTracked) {
                Debug.DrawRay(astroPos, dir:tb.transform.right, Color.red);
            }
        }

    }

}