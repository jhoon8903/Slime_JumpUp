using System;

namespace Events
{
    public class CharacterEventHandler
    {
        public event Action CharacterRespawn;
        public event Action<float> CharacterHeight; 
        public void OnCharacterRespawn()
        {
            CharacterRespawn?.Invoke();
        }

        public void OnUpdateHeight(float height)
        {
            CharacterHeight?.Invoke(height);
        }
    }
}