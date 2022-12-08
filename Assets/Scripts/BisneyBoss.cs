using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BisneyBoss : MonoBehaviour
{
    public GameObject spin, detectionBar, detectionBox, visualBar, blastSpawnPoint1, blastSpawnPoint2;
    public GameObject targetMarkerPref;
    public Slider healthBar;
    private GameObject targetMarker;
    private bool boolBlastPoint = false;
    
    enum BossState { normal, run, follow, chargeJump, middle };
    BossState bossState = BossState.run;
    public float health = 333;
    private FieldOfView fov;
    private GameObject player;
    private float lookSpeed = 200;
    NavMeshAgent agent;
    public Transform[] waypoints;
    public GameObject maypointMid;
    int waypointindex;
    int currentWaypoint = 0;
    Vector3 target;

    private float normalAttackTime = 0;
    private float normalAttackTime2 = 0;
    private bool middleAttackStarted = false;

    
    private float fltBulletSpeed = 10;
    public float shootDelay = .2f;
    private bool shot = false;
    public GameObject bullet;
    public VisualEffect VFX1, VFX2;
    private float fColorAmt = 200;
    private float fDestroy = 20;

    // damage 
    private float jumpDamage = 30;
    private float spinDamage = 30;
    private float blastDamage = 10;
    private IEnumerator middleAttackCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        fov = GetComponent<FieldOfView>();
        player = GameObject.Find("Player");
        healthBar.value = health;

    }

    // Update is called once per frame
    void Update()
    {
        switch (bossState)
        {
            case BossState.run:
                if (Vector3.Distance(transform.position, target) < 1)
                {
                    normalAttackTime = UnityEngine.Random.Range(5, 10);
                    normalAttackTime2 = normalAttackTime - 1;
                    bossState = BossState.normal;
                }
                break;
            case BossState.normal:
                if (normalAttackTime > 0)
                {
                    Vector3 newDirection = player.transform.position - gameObject.transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(newDirection);
                    Quaternion lookAt = Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
                    gameObject.transform.rotation = lookAt;
                    normalAttackTime -= Time.deltaTime;
                    //Debug.Log(normalAttackTime);
                    if (!shot && normalAttackTime < normalAttackTime2) // shoot
                        StartCoroutine(shoot());
                }
                else
                {
                    switch (UnityEngine.Random.Range(0, 100))
                    {
                        case int n when (n <= -1):
                            bossState = BossState.follow;
                            SetVFXColor("g");
                            break;
                        case int n when (n <= 40):
                            target = maypointMid.transform.position;
                            agent.SetDestination(target);
                            bossState = BossState.middle;
                            SetVFXColor("b");
                            break;
                        case int n when (n <= 90):

                            if (Vector3.Distance(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z)) > 10)
                            {
                                targetMarker = Instantiate(targetMarkerPref, new Vector3(player.transform.position.x, -1, player.transform.position.z), new Quaternion(0, 0, 0, 0));
                                bossState = BossState.chargeJump;
                                agent.isStopped = true;
                                StartCoroutine(ChargeJump());
                                SetVFXColor("r");
                            }
                            else
                            {
                                ToRun();
                            }
                            break;
                        default:
                            ToRun();
                            break;
                    }

                }
                break;
            case BossState.follow:
                target = player.transform.position;
                agent.SetDestination(target);
                if (Vector3.Distance(transform.position, target) < 1)
                {
                    normalAttackTime = UnityEngine.Random.Range(5, 10);
                    normalAttackTime2 = normalAttackTime - 1;
                    bossState = BossState.normal;
                }
                break;
            case BossState.middle:
                if (Vector3.Distance(transform.position, target) < 1 && !middleAttackStarted)
                {
                    middleAttackStarted = true;
                    visualBar.SetActive(true);
                    detectionBar.SetActive(true);
                    middleAttackCoroutine = MiddleAttack();
                    StartCoroutine(middleAttackCoroutine);
                }

                break;
        }

    }
    void ToRun()
    {
        int newWaypoint;
        do
        {
            newWaypoint = UnityEngine.Random.Range(0, 3);
        } while (newWaypoint == currentWaypoint);
        currentWaypoint = newWaypoint;
        target = waypoints[newWaypoint].transform.position;
        agent.SetDestination(target);
        bossState = BossState.run;
        //VFX1.GetComponent<VisualEffect>().CreateVFXEventAttribute(agent,);
        SetVFXColor("g");
    }
    void UpdateDestination() // updates the agent with waypoint
    {
        target = waypoints[waypointindex].position;
        agent.SetDestination(target);
    }
    void IterateWaypointIndex() // change to next waypoint
    {
        waypointindex++;
        if (waypointindex == waypoints.Length)
        {
            waypointindex = 0;
        }
    }

    private IEnumerator shoot() // fire a bullet where enemy is looking
    {
        //System.Random rnd = new System.Random();
        //GameObject newBullet = Instantiate(bullet, gun.transform.position, new Quaternion((float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100 + gun.transform.rotation.x, gun.transform.rotation.y + (float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100, gun.transform.rotation.z, gun.transform.rotation.w));

        GameObject point = boolBlastPoint ? blastSpawnPoint1 : blastSpawnPoint2;
        boolBlastPoint = !boolBlastPoint;

        point.transform.LookAt(player.transform.position);
        GameObject newBullet = Instantiate(bullet, point.transform.position, point.transform.rotation);
        newBullet.GetComponent<PlasmaBallScript>().StatDestroy(fDestroy);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fltBulletSpeed;
        newBullet.GetComponent<PlasmaBallScript>().damage = blastDamage;
        shot = true;
        yield return new WaitForSeconds(shootDelay);
        shot = false;
    }
    private IEnumerator MiddleAttack() // fire a bullet where enemy is looking
    {
        float startRotation = spin.transform.eulerAngles.y;
        for (int i = 0; i < 360; i++)
        {
            //Debug.Log(i);
            //spin.transform.rotation = new Quaternion(0,10 * i,0, spin.transform.rotation.w);
            //////spin.transform.eulerAngles = new Vector3(transform.eulerAngles.x, startRotation + i, transform.eulerAngles.z);
            spin.transform.eulerAngles = new Vector3(90, startRotation + i, 180);
            //spin.transform.rotation =  Quaternion.Lerp(new Quaternion(0, 0, 0, 0),new Quaternion(0, 360, 0, 0), i/36);
            yield return new WaitForSeconds(.01f);
        }

        ToRun();
        middleAttackStarted = false;
        visualBar.SetActive(false);
        detectionBar.SetActive(false);
        spin.transform.eulerAngles = new Vector3(90, 0, 180);
    }
    private IEnumerator ChargeJump() // fire a bullet where enemy is looking
    {
        yield return new WaitForSeconds(1f);
        agent.enabled = false;
        Vector3 start = transform.position;
        Vector3 end = targetMarker.transform.position;

        for (float i = .05f; i < 1.05; i += .05f)
        {
            //transform.position = new Vector3(Mathf.Lerp(startVector.x, targetMarker.transform.position.x, i / 10), 0, Mathf.Lerp(startVector.y, targetMarker.transform.position.y, i / 10));
            //float fI = i / 10;
            //Debug.Log(Mathf.Pow(Mathf.Lerp(1, 10, i <= .5f ? (i * 10f) : 10f - (i * 10f)), .5f));

            //                                     Debug.Log(Mathf.Sin(Mathf.PI * i));
            Vector3 pos = Vector3.Lerp(start, end, i);
            transform.position = new Vector3(pos.x, Mathf.Sin(Mathf.PI * i) * 5 + .5f, pos.z);
            //transform.position = new Vector3(pos.x,Mathf.Pow(Mathf.Lerp(1,10, i <= .5f ? i*10: 10 - i*10),.7f), pos.z);
            yield return new WaitForSeconds(.1f);
        }
        agent.enabled = true;
        ToRun();
        agent.isStopped = false;
        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            player.GetComponent<PHealth>().TakeDamage(jumpDamage);
            //GameObject aud = Instantiate(audPlayer);
            //aud.transform.position = collision.transform.position;
            //aud.GetComponent<DestroySelf>().DestroyObject(1);
        }
        Destroy(targetMarker);
    }
    public void DamagePlayer(float damage)
    {
        //if(!player == null)

    }
    private void SetVFXColor(string str)
    {
        if (VFX1 == null || VFX2 == null)
            return;
        VFX1.SetFloat("r", str == "r" ? fColorAmt : 0);
        VFX2.SetFloat("r", str == "r" ? fColorAmt : 0);
        VFX1.SetFloat("g", str == "g" ? fColorAmt : 0);
        VFX2.SetFloat("g", str == "g" ? fColorAmt : 0);
        VFX1.SetFloat("b", str == "b" ? fColorAmt : 0);
        VFX2.SetFloat("b", str == "b" ? fColorAmt : 0);
    }
   public void TakeDamage(float newDamage)
    {
        health -= newDamage;
        healthBar.value = health;
        if(health <= 0)
        {
            SceneManager.LoadScene("Win Screen");
        }
    }
    public void BarHitPlayer()
    {
        StopCoroutine(middleAttackCoroutine);
        ToRun();
        middleAttackStarted = false;
        visualBar.SetActive(false);
        detectionBar.SetActive(false);
        spin.transform.eulerAngles = new Vector3(90, 0, 180);
        player.GetComponent<PHealth>().TakeDamage(spinDamage);
    }
}
//i > 5 ? Mathf.Lerp(5, 0, i / 5): Mathf.Lerp(0, 5, i/5)