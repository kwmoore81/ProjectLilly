using UnityEngine;

public interface IHeroActionControl
{
    void HeroAwake();

    void DrawWeapon();
    void AttackInput(int _attackID, Vector3 _targetPosition);
    void ItemUseInput(int _itemID);
    void DefendInput();
    void HitReaction();
    void DeathReaction();

}
