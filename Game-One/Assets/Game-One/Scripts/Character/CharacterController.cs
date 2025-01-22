using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class CharactorController : MonoBehaviour
    {
        private CharacterBase linkedCharactor;
        private InteractionSensor interactionSensor;

        public LayerMask groundLayer;

        private void Awake()
        {
            linkedCharactor = GetComponent<CharacterBase>();
            interactionSensor = GetComponent<InteractionSensor>();
        }

        private void Start()
        {
            InputSystem.Singleton.OnLeftMouseButtonDown += LeftMouseButtonEvent;
            InputSystem.Singleton.OnRightMouseButtonDown += RightMouseButtonEvent;
            InputSystem.Singleton.OnSpaceInput += SpaceInputEvent;
            InputSystem.Singleton.OnKeyInput += KeyInputEvent;
            InputSystem.Singleton.OnLootingInput += LootingInputEvent;

            InitializeSkills();

            linkedCharactor.Initialize();
            IngameUI.Instance.SetHP(linkedCharactor.currentHP, linkedCharactor.maxHP);
            IngameUI.Instance.SetSP(linkedCharactor.currentSP, linkedCharactor.maxSP);
            IngameUI.Instance.Init(linkedCharactor.skills);
        }

        private void LootingInputEvent()
        {
            if (interactionSensor.detectedPickupItems.Count <= 0)
                return;

            IBox firstPickupItem = interactionSensor.detectedPickupItems[0];
            linkedCharactor.Looting(firstPickupItem.InterationPoint.position, firstPickupItem.InterationPoint.rotation);
            firstPickupItem.Open();
        }

        private void InitializeSkills()
        {
            if (GameDataModel.Singleton.GetSkillData("DeathFire", out var deathFireDataSO))
            {
                var deathFireSkill = new Skill_DeathFire();
                deathFireSkill.Init(deathFireDataSO);
                linkedCharactor.AddSkill(KeyCode.Q, deathFireSkill);
            }
            if (GameDataModel.Singleton.GetSkillData("QuickStep", out var quickStepDataSO))
            {
                var quickStepSkill = new Skill_QuickStep();
                quickStepSkill.Init(quickStepDataSO);
                linkedCharactor.AddSkill(KeyCode.W, quickStepSkill);
            }
            if (GameDataModel.Singleton.GetSkillData("PlasmaBullet", out var plasmaBulletDataSO))
            {
                var plasmaBulletSkill = new Skill_PlasmaBullet();
                plasmaBulletSkill.Init(plasmaBulletDataSO);
                linkedCharactor.AddSkill(KeyCode.E, plasmaBulletSkill);
            }
            if (GameDataModel.Singleton.GetSkillData("PieceKeeper", out var pieceKeeperDataSO))
            {
                var pieceKeeperSkill = new Skill_PieceKeeper();
                pieceKeeperSkill.Init(pieceKeeperDataSO);
                linkedCharactor.AddSkill(KeyCode.R, pieceKeeperSkill);
            }

            if (GameDataModel.Singleton.GetSkillData("PerfectShot", out var perfectShotDataSO))
            {
                var perfectShotSkill = new Skill_PerfectShot();
                perfectShotSkill.Init(perfectShotDataSO);
                linkedCharactor.AddSkill(KeyCode.A, perfectShotSkill);
            }
            if (GameDataModel.Singleton.GetSkillData("TargetDown", out var targetDownDataSO))
            {
                var targetDownSkill = new Skill_TargetDown();
                targetDownSkill.Init(targetDownDataSO);
                linkedCharactor.AddSkill(KeyCode.S, targetDownSkill);
            }
            if (GameDataModel.Singleton.GetSkillData("Deadshot", out var deadShotDataSO))
            {
                var deadShotSkill = new Skill_Deadshot();
                deadShotSkill.Init(deadShotDataSO);
                linkedCharactor.AddSkill(KeyCode.D, deadShotSkill);
            }
            if (GameDataModel.Singleton.GetSkillData("PhantomStrike", out var phantomStrikeDataSO))
            {
                var phantomStrikeSkill = new Skill_PhantomStrike();
                phantomStrikeSkill.Init(phantomStrikeDataSO);
                linkedCharactor.AddSkill(KeyCode.F, phantomStrikeSkill);
            }

        }


        private void OnDestroy()
        {
            if (InputSystem.Singleton)
            {
                InputSystem.Singleton.OnLeftMouseButtonDown -= LeftMouseButtonEvent;
                InputSystem.Singleton.OnRightMouseButtonDown -= RightMouseButtonEvent;
                InputSystem.Singleton.OnSpaceInput -= SpaceInputEvent;
                InputSystem.Singleton.OnKeyInput -= KeyInputEvent;
                InputSystem.Singleton.OnLootingInput -= LootingInputEvent;
            }
        }

        private void SpaceInputEvent()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000F, groundLayer, QueryTriggerInteraction.Ignore))
            {
                Vector3 direction = (hitInfo.point - linkedCharactor.transform.position).normalized;
                float yAxisAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                linkedCharactor.Dash(yAxisAngle);
            }

        }

        /// <summary>
        /// 키보드 입력 이벤트 일괄 등록
        /// </summary>
        private void KeyInputEvent(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Q:
                case KeyCode.W:
                case KeyCode.E:
                case KeyCode.R:
                case KeyCode.A:
                case KeyCode.S:
                case KeyCode.D:
                case KeyCode.F:
                    KeyboardSkillKeyEvent(keyCode);
                    break;

            }
        }


        /// <summary>
        /// QWERASDF 스킬 이벤트 
        /// </summary>
        private void KeyboardSkillKeyEvent(KeyCode keyCode)
        {
            // TODO: Keycode 값에 해당하는 스킬의 쿨다운 값을 확인해서 스킬을 사용할 수 있는 상태인지 확인하기


            // 마우스 포인터 방향으로 공격액션 수행하도록 적용
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000F, groundLayer, QueryTriggerInteraction.Ignore))
            {
                Vector3 direction = (hitInfo.point - linkedCharactor.transform.position).normalized;
                float yAxisAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                linkedCharactor.SkillAttack(yAxisAngle, keyCode);
            }
        }
        /// <summary>
        /// 마우스 좌클릭 시 기본 공격 모션 이벤트
        /// </summary>
        private void LeftMouseButtonEvent()
        {
            // 마우스 포인터 방향으로 공격액션 수행하도록 적용
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000F, groundLayer, QueryTriggerInteraction.Ignore))
            {
                Vector3 direction = (hitInfo.point - linkedCharactor.transform.position).normalized;
                float yAxisAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                linkedCharactor.NormalAttack(yAxisAngle);
            }
        }
        /// <summary>
        /// 마우스 우클릭 시 캐릭터 이동 이벤트
        /// </summary>
        private void RightMouseButtonEvent()
        {
            // TODO : 카메라 상에서 보았을 때, 우클릭 위치[스크린상에서의 위치] => 3D 공간에서의 위치로 변환
            // TODO : LinkedCharacter 한테 이동 명령[좌표 전달]을 한다.

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, groundLayer, QueryTriggerInteraction.Ignore))
            {
                // Ray 가 Ground Layer에 부딫힌 경우
                linkedCharactor.SetDestination(hitInfo.point);
                lastRightClickPoint = hitInfo.point;
            }
        }

        private Vector3 lastRightClickPoint;
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
            Gizmos.DrawSphere(lastRightClickPoint, 1f);
        }
    }
}
