namespace Obstacles
{
    public class Stone : Obstacle
    {
        protected override void OnEnable()
        {
            Delay = 5f;
            base.OnEnable();
        }
    }
}