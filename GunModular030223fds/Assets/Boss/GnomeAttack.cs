using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Wormega/Attack/Gnome")]
public class GnomeAttack : Attack
{
    public float delay;
    public GameObject Spiner;

    public override void Execute(Enemy en)
    {
        base.Execute(en);

        Gnome gnome = (Gnome)en;

        gnome.NavMeshAgent.enabled = false;

        gnome.ShootFX();

        en.StartCoroutine(MoveLine(gnome));
    }

    public IEnumerator MoveLine(Gnome gnome)
    {
        yield return new WaitForSeconds(1f);

        Vector3 playerPosition = gnome.player.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(playerPosition, out hit, 100f, NavMesh.AllAreas))
        {
            Vector3 finalPosition = hit.position + new Vector3(0f, 20f, 0f);

            gnome.lm2.positionCount = 2;
            gnome.lm2.SetPosition(0, finalPosition);

            gnome.NavMeshAgent.enabled = false;

            LineRenderer lr = gnome.lm2;

            lr.SetPosition(1, finalPosition);
            lr.enabled = true;

            float moveStartTime = Time.time;
            float delays = gnome.moveDuration;
            Vector3 newPosition = playerPosition;

            while (Time.time - moveStartTime < delays)
            {
                float progress = (Time.time - moveStartTime) / delays;

                playerPosition = gnome.player.transform.position;

                if (NavMesh.SamplePosition(playerPosition, out hit, 100f, NavMesh.AllAreas))
                {
                    Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)); // Adjust the range as desired
                    newPosition = Vector3.Lerp(finalPosition, hit.position + randomOffset, progress);

                    lr.SetPosition(1, newPosition);
                }

                yield return null;
            }

            lr.SetPosition(1, newPosition);
            GameObject g = GameObject.Instantiate(Spiner);
            gnome.lm.enabled = false;
            gnome.NavMeshAgent.enabled = true;
            g.transform.position = newPosition;

            yield return new WaitForSeconds(1.5f);

            Destroy(g);
            lr.enabled = false;
            
        }
    }
}
