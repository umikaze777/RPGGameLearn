﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;

        private void Start() {
            health = GetComponent<Health>();
        }
        // Update is called once per frame
        void Update()
        {
            if(health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            Debug.Log("Nothing to do");
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if(!target) continue;

                GameObject targetGameObject = target.gameObject;
                if(!GetComponent<Fighter>().CanAttack(targetGameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(targetGameObject);
                }
                return true;
            }
            return false;
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}