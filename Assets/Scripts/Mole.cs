using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float damage = 10;
    public ParticleSystem damageParticle;
    Obstacle attackedObstacle;

    public float attackSpeed = 0.2f;

    void Update()
    {
        LookAtMouse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            attackedObstacle = collision.GetComponent<Obstacle>();
            StartCoroutine(AttackObstacle());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attackedObstacle = null;
        damageParticle.Stop();
    }

    void Attack()
    {
        attackedObstacle.DealDamage(damage);
    }

    void LookAtMouse()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator AttackObstacle()
    {
        damageParticle.Play();
        Attack();
        yield return new WaitForSeconds(attackSpeed);
        if (attackedObstacle != null)
        {
            StartCoroutine(AttackObstacle());
        }
    }
}
