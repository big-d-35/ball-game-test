using System;
using UniRx;
using UnityEngine;

public class BallController : ABallController
{
    [SerializeField] Rigidbody body;
    [SerializeField] private ABall ball;

    public override ABall Ball => ball;

    public override void Place(Vector3 position)
    {
        body.isKinematic = true;
        body.MovePosition(position);
        BallEvents.onBallPlaced.Execute(body.position);
    }

    public override void Push(Vector3 direction)
    {
        body.isKinematic = false;
        body.AddForce(direction * ball.Strength);
        BallEvents.onPushed.Execute(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            CheckBallStop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ring"))
        {
            BallEvents.onGoal.Execute();
        }
    }



    private CompositeDisposable onGroundedCheckComposite = new CompositeDisposable();
    private void CheckBallStop(){
        float timer = 0;
        Observable.EveryUpdate().Subscribe(_ => 
        {
            timer += Time.deltaTime;

            if(Vector3.Magnitude(body.linearVelocity) <= Ball.StopFactor || (timer >= Ball.StopTime && Ball.StopTime >= 0))
            {
                BallEvents.onStopped.Execute(body.position);
                onGroundedCheckComposite.Clear();
            }
        }).AddTo(onGroundedCheckComposite);
    }
}