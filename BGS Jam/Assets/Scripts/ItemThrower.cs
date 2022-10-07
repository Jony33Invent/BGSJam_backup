using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemThrower : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private ItemCollector collector;
    Vector3 throwDirection;
    [SerializeField] private float impulse;
    [SerializeField] private float impulseUp;
    private bool aimState = false;

    [SerializeField] private LineRenderer aimLine;
    
    private void Fire(InputAction.CallbackContext ctx)
    {
        if (aimState) // THROW ITEM
        {
            collector.ThrowItem((player.Body.forward + Vector3.up * impulseUp) * impulse);
            player.SetCanMove(true);
            aimState = false;
            aimLine.enabled = false;
            player.SetAiming(false);
        }
    }
    private void StartAim(InputAction.CallbackContext ctx)
    {
        if (collector.HasItem)
        {
            aimState = true;
            aimLine.enabled = true;
            player.SetCanMove(false);
            player.SetAiming(true);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

        player.controls.Player.Fire.started += StartAim;
        player.controls.Player.Fire.canceled += Fire;
    }
    [SerializeField] private float maxImpulse;
    [SerializeField] private float tOffset=0.2f;
    [SerializeField] private int maxParabolaPoints = 15;
    public void DrawParabola()
    {
        impulse = Vector3.Distance(collector.transform.position, player.AimPoint)+0.1f;
        impulse = Mathf.Min(impulse, maxImpulse);
        float angle = Vector3.Angle(player.Body.forward,( player.Body.forward + Vector3.up* impulseUp))*Mathf.Deg2Rad;
        float v0 = ((player.Body.forward + Vector3.up* impulseUp) * impulse).magnitude;
        float maxDist = Mathf.Abs((v0 * v0 * Mathf.Sin(2*angle)) / Physics.gravity.y);
        float height = Mathf.Abs((v0*v0* Mathf.Pow(Mathf.Sin( angle),2)) / (2*Physics.gravity.y));


        Vector3 S0= collector.transform.position;
        Vector3 V0 = (player.Body.forward + Vector3.up * impulseUp) * impulse;
        Vector3 a = Physics.gravity;
        Vector3 final = collector.transform.position + (player.Body.forward) * maxDist;

        float t = 0;
        Vector3 S;
        List<Vector3> pos=new List<Vector3>();
        int n = 0;
        do
        {
            S = S0 + V0 * t + 0.5f * t * t * a;
            pos.Add(S);
            t += tOffset;
            n++;

        } while (n< maxParabolaPoints);
        aimLine.positionCount = n;
        for(int i = 0; i < n; i++)
        {
            aimLine.SetPosition(i, pos[i]);
        }
        /*
        aimLine.SetPosition(1, collector.transform.position+ (((player.Body.forward)* maxDist)/2+Vector3.up*height));
        aimLine.SetPosition(2, collector.transform.position + (player.Body.forward) * maxDist);*/
    }
    // Update is called once per frame
    void Update()
    {
        if (aimState)
        {
            DrawParabola();
        }
    }
}
