using UnityEngine;

 namespace PhxEngine2D.Sample{

    public class SampleEntity : MonoBehaviour{

        void OnCollisionEnter2D(Collision2D collision){
            Debug.Log("OnCollisionEnter2D");
        }
    }
 }   