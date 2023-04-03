using Godot;

namespace Thomas
{
    public static class Node2DExtensions
    {
        public static void ClampToScreen(this Node2D node, Vector2 screenSize)
        {
            node.Position = new Vector2
            {
                x = Mathf.Clamp(node.Position.x, 0, screenSize.x),
                y = Mathf.Clamp(node.Position.y, 0, screenSize.y)
            };
        }
    }
}
