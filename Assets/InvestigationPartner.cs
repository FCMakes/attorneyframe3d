using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvestigationPartner : MonoBehaviour
{
    public GameObject Player;
    public GameObject Standing;
    public GameObject Moving;
    public float DistanceToPlayer;
    public int State;
    public float closestDistance;
    public float walkDistance;
    public void Update()
    {
        if (Player.GetComponent<MoveController>().enabled)
        {

            DistanceToPlayer = Vector3.Distance(base.gameObject.transform.position, Player.transform.position);

            if (DistanceToPlayer > closestDistance)
            {

                if (State == 0 || State == 1)
                {
                    if (DistanceToPlayer > walkDistance)
                    {
                        State = 2;
                    }
                    else
                    {
                        State = 1;
                    }
                }


            }
            else
            {
                State = 0;
            }

            if (State == 0)
            {
                Moving.SetActive(false);
                Standing.SetActive(true);
                base.gameObject.GetComponent<NavMeshAgent>().isStopped = true;

                GameObject victim = base.gameObject;
                GameObject at = Player;

                Vector3 targetPostition = new Vector3(at.transform.position.x,
                                               victim.transform.position.y,
                                                 at.transform.position.z);


                victim.transform.LookAt(targetPostition);



            }
            else
            {
                Standing.SetActive(false);
                Moving.SetActive(true);
                base.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                base.gameObject.GetComponent<NavMeshAgent>().SetDestination(Player.gameObject.transform.position);
            }

            Moving.GetComponent<Animator>().SetInteger("State", State);
            base.gameObject.GetComponent<NavMeshAgent>().speed = State * 7;

        }
        else
        {

            Moving.SetActive(false);
            Standing.SetActive(true);
            base.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            State = 0;
            base.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        }
    }
}
