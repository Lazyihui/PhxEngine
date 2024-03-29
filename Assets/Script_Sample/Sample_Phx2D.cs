using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhxEngine2D;

public class Sample_Phx2D : MonoBehaviour {
    [SerializeField] GameObject circle;
    [SerializeField] GameObject circle2;
    [SerializeField] GameObject floor;

    Phx2D phx;
    RBEntity rbCircle;
    RBEntity rbCircle2;
    RBEntity floorSquare;
    void Start() {
        phx = new Phx2D();
        phx.OnTriggerEnterHandle = (a, b) => {
            Debug.Log("OnIntersectEnterHandle"+a.id+" "+b.id);
        };

        phx.OnTriggerStayHandle = (a, b) => {
            Debug.Log("OnIntersectStayHandle"+a.id+" "+b.id);
        };

        phx.OnTriggerExitHandle = (a, b) => {
            Debug.Log("OnIntersectExitHandle"+a.id+" "+b.id);
        };

        rbCircle = phx.Add(1, ShapeType.Circle, new Vector2(1, 1));
        rbCircle.gravityScale = 1;
        rbCircle.position = circle.transform.position;

        rbCircle2 = phx.Add(3, ShapeType.Circle, new Vector2(1, 1));
        rbCircle2.gravityScale = 1;
        rbCircle2.position = circle2.transform.position;
        rbCircle2.isStatic = true;

        floorSquare = phx.Add(2, ShapeType.Square, new Vector2(10, 1));
        floorSquare.gravityScale = 0;
        floorSquare.position = floor.transform.position;
        floorSquare.isStatic = true;
    }

    void Update() {
        float dt = Time.deltaTime;

        phx.Tick(dt);

        circle.transform.position = rbCircle.position;
        circle2.transform.position = rbCircle2.position;
        floor.transform.position = floorSquare.position;
    }

    void OnDrawGizmos() {
        // ？这个用法
        rbCircle?.DrawGizmos();
        rbCircle2?.DrawGizmos();
        floorSquare?.DrawGizmos();
    }
}
