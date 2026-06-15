using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 부딪힌 오브젝트가 플레이어(유니티짱)라면 게임오버 호출
        if (other.CompareTag("Player") || other.GetComponentInParent<KinematicCharacterController.Examples.ExampleCharacterController>() != null)
        {
            GameController.instance.GameOver();
        }
    }
}