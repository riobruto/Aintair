using GameSystem;

namespace Interfaces
{
    public interface IHittableFromBomb
    {
        public void OnBombHit(BombHitPayload payload);
    }
}