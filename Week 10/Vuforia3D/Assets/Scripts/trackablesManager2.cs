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
        line.widthMultiplier = .005f;
        line.positionCount = 2;
        line.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.enabled = false;

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
                // Debug.DrawRay(astroPos, dir:(moonPos - astroPos), Color.green);
                
                if (Physics.Raycast(transform.position, (moonPos - astroPos), out hit)) {
                        Debug.DrawRay(astroPos, (moonPos - astroPos), Color.green);
                         line.enabled = true;
                        var points = new Vector3[2];
                        points[0] = astroPos;
                        points[1] = moonPos;
                        line.SetPositions(points);

                } else { 
                    line.enabled = false;
                }
                

                spaceShip.transform.position += (moonPos - astroPos).normalized * (.1f * Time.deltaTime);
            } 
            
            if (astroTracked == true && moonTracked == false ) {
                Debug.DrawRay(astroPos, dir:tb.transform.right, Color.red);
            }
        }

    }

}