using UnityEngine;

public interface IHeroActionControl
{
    void HeroAwake();

    void DrawWeapon();
    void AttackInput(BaseAttack _chosenAttack, Vector3 _targetPosition);
    void ItemUseInput(int _itemID);
    void DefendInput();
    void HitReaction();
    void DeathReaction();

}
