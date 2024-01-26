using GameSystem;

namespace Interfaces
{
    public interface IHittableFromWeapon
    {
        public void OnHit(HitPayload payload);
    }
}