using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Wormega/Attacks/BallerAttack")]
public class BallerAttack : Attack
{
    public float BaseDam;
    public float ShootDelay;
    public float Delay;
    public GameObject TargetPrefab;
    public GameObject ExplosionVFX;
    public Vector3 endScale;
    Coroutine c = null;
    public override void Execute(Enemy en)
    {
        en.NavMeshAgent.isStopped = true;
        Baller b = (Baller)en;
        en.StartCoroutine(CanonShoot(b,Delay));
      
    }

    public IEnumerator CanonShoot(Baller b,float t)
    {
        List<Tube> tubes = new List<Tube>();
        tubes.Add(b.Tube1);
        tubes.Add(b.Tube2);
        tubes.Add(b.Tube3);
        tubes.Add(b.Tube4);
        tubes.Add(b.Tube5);

        foreach (var ta  in tubes)
        {
            ta.VFX.Play();
            AudioUtils.PlaySoundWithPitch(ta.As,b.MissleShoot[Random.Range(0,b.MissleShoot.Length)],1f);
            yield return new WaitForSeconds(ShootDelay);
        }
        yield return new WaitForSeconds(t);

        Vector3[] ranPoints = new Vector3[5];
        NavMeshHit hit;
        GameObject g = null;
        if (NavMesh.SamplePosition(b.player.transform.position, out hit, 100f, NavMesh.AllAreas))
        {
            // The NavMesh point is hit.position
            Vector3 navMeshPoint = hit.position;
            g = GameObject.Instantiate(TargetPrefab, navMeshPoint, Quaternion.identity);
            g.transform.eulerAngles = new Vector3(90f, 0f, 0f);
                Debug.Log("Nearest NavMesh point: " + navMeshPoint);
                
            for (int i = 0; i < 5; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle * 1f;
                Vector3 randomPoint = navMeshPoint + new Vector3(randomCircle.x, 0, randomCircle.y);
                NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas);
                ranPoints[i] = hit.position;
            }
        }
        else
        {
            Debug.LogError("Could not find a point on the NavMesh!");
        }
        
        Vector3 startScale = g.transform.localScale;
        for (int i = 0; i < 5; i++)
        {
            float time = 0f;
            g.transform.localScale = startScale;
            g.transform.position = ranPoints[i];
            while (time < .5f)
            {
                float te = time / .5f;
                g.transform.localScale = Vector3.Lerp(startScale, endScale, te);
                time += Time.deltaTime;
                yield return null;
            }

            GameObject.Instantiate(ExplosionVFX, g.transform.position, quaternion.identity);
            Collider[] tea = Physics.OverlapSphere(g.transform.position, 2f);

            foreach (Collider item in tea)
            {
                if (item.tag == "Player" || item.tag == "Destructable")
                    item.GetComponent<Damageable>().DoDamage(Damage(item.transform.position, b.transform.position,b),Vector3.zero);
            }

        }

        Destroy(g);
        c = null;
        base.Execute(b);
    }
    public float Damage(Vector3 target, Vector3 self, Enemy e)
    {
        float distance = Vector3.Distance(self, target);
        float damage = e.EnemyStats.maxAttackDamage / distance;
        Debug.Log(damage);

        if (damage > BaseDam)
            return BaseDam;
        else return damage;
    }


}
