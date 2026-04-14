using UnityEngine;
// INHERITANCE
public class EnemyShooter : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // POLYMORPHISM
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOutOfBounds();
        MoveObjects();
    }
}