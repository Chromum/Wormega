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

        gnome.ShootFX();


        NavMeshHit hit;
        if (NavMesh.SamplePosition(en.player.transform.position, out hit, 100f, NavMesh.AllAreas))
        {
            Vector3 playerPosition = hit.position;
            Vector3 finalPosition = (playerPosition + new Vector3(0f, 20f, 0f));

            gnome.lm2.positionCount = 2;
            gnome.lm2.SetPosition(0, finalPosition);

            en.StartCoroutine(MoveLine(finalPosition, playerPosition, gnome.moveDuration, gnome.lm2, gnome));
        }
           

        
    }



    public IEnumerator MoveLine(Vector3 Start, Vector3 end,float delays,LineRenderer lr, Gnome go)
    {

        yield return new WaitForSeconds(1f);

        lr.SetPosition(1, Start);
        lr.enabled = true;

        float moveStartTime = Time.time;

        while (Time.time - moveStartTime < delays)
        {
            // Calculate the current progress
            float progress = (Time.time - moveStartTime) / delays;

            // Interpolate the position based on the progress
            Vector3 newPosition = Vector3.Lerp(Start, end, progress);

            // Update the LineRenderer position
            lr.SetPosition(1, newPosition);

            yield return null;
        }

        // Set the final position to ensure accuracy
        lr.SetPosition(1, end);
        GameObject g = GameObject.Instantiate(Spiner);
        g.transform.position = end;

        yield return new WaitForSeconds(1.5f);

        Destroy(g);
        lr.enabled = false;
        go.lm.enabled = false;
        
    }
}
