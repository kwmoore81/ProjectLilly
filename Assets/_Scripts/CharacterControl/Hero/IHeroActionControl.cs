using UnityEngine;

public interface IHeroActionControl
{
    void HeroAwake();

    void DrawWeapon();
    void AttackInput(AttackData _chosenAttack, Vector3 _targetPosition);
    void ItemUseInput(int _itemID);
    void DefendInput();
    void HitReaction();
    void DeathReaction();

}
