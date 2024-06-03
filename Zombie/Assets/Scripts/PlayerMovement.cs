﻿using TreeEditor;
using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        Rotate(); 
        Move();
        //입력값에 따라 애니메이터의 Move 파라미터 값 변경
        playerAnimator.SetFloat("Move", playerInput.move);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        // 상대적으로 이동하는 거리 게산
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        // 리지드 바디를 이용해 게임 오브젝트 위치 변경
        // 이동시 벽이나 물체(충돌체)가 존재할 경우 밀어내거나 밀려나는 물리를 적용하여 이동
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        // 물리처리 무시하고 위치 변경
        //transform.position = transform.position + moveDistance; 

    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {
        // 상대적으로 회전할 수치 계산
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        // 리지드바디를 이용해 게임 오브젝트 회전 변경
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
        // 물리처리를 무시하고 위치 변경
        //transform.rotation = transform.rotation * Quaternion.Euler(0f, turn, 0f);
    }
}