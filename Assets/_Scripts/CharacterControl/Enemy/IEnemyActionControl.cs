using UnityEngine;

public interface IEnemyActionControl
{
    void EnemyAwake();

    void DrawWeapon();
    void Revive();
    void AttackInput(int _attackID, Vector3 _targetPosition);
    void ItemUseInput(int _itemID);
    void DefendInput();
    void HitReaction();
    void DeathReaction();

}
