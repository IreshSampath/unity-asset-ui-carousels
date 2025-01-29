using UnityEngine;
using UnityEngine.UI;

public class CustomRaycastFilter : Image
{
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        // Convert the screen point to local coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, screenPoint, eventCamera, out Vector2 localPoint);

        // Normalize the local point to [0, 1]
        Vector2 normalizedPoint = new Vector2(
            (localPoint.x + rectTransform.pivot.x * rectTransform.rect.width) / rectTransform.rect.width,
            (localPoint.y + rectTransform.pivot.y * rectTransform.rect.height) / rectTransform.rect.height
        );

        // Get the current sprite from the Image component
        Sprite sprite = this.sprite; // Access the sprite from the Image component
        if (sprite == null) return true; // Allow raycasts if no sprite is assigned

        // Get the texture coordinates within the sprite
        Rect spriteRect = sprite.textureRect;
        float x = spriteRect.x + normalizedPoint.x * spriteRect.width;
        float y = spriteRect.y + normalizedPoint.y * spriteRect.height;

        // Check the alpha value of the pixel at the normalized position
        Color color = sprite.texture.GetPixel((int)x, (int)y);
        return color.a > 0.1f; // Only allow raycasts on non-transparent pixels
    }
}
