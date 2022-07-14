using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletPos;
    public Transform player;
    private bool _canTurretAttack = false;
    private float _elapsedTime = 0.0f;
    private float _stdTime = 0.5f;
    

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            Vector3 directionVec = (player.position - player.transform.position).normalized;
            float direc = Vector3.Dot(player.transform.forward, directionVec);// 내적
            Vector3 distanceVector = player.position - transform.position; // 두 벡터의 거리
            Vector3 vecCross = Vector3.Cross(transform.forward.normalized, distanceVector.normalized); // 외적
        
            _canTurretAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _canTurretAttack = false;
        }
    }
    private void TurretAttack()
    {
        transform.LookAt(player.transform);
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= _stdTime)
        {
            _elapsedTime = 0.0f;
            GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
        }
    }

    void TurretSpin()
    {
        transform.Rotate(0f, _stdTime, 0f);
        _elapsedTime = _stdTime;
    }
    
    void Update()
    {
        
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.blue);

        if(_canTurretAttack && player.gameObject.activeSelf)
        {
            TurretAttack();
        }
        
    }
}
