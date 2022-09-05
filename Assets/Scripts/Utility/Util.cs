using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // General Utility Class

    // Converts From Sprite To Texture2D
    public static Texture2D textureFromSprite(Sprite sprite, float sizeScale)
     {
         if(sprite.rect.width != sprite.texture.width){
             Texture2D newText = new Texture2D((int)(sprite.rect.width*sizeScale),(int)(sprite.rect.height*sizeScale));
             Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
                                                          (int)sprite.textureRect.y, 
                                                          (int)sprite.textureRect.width, 
                                                          (int)sprite.textureRect.height );
             newText.SetPixels(newColors);
             newText.Apply();
             return newText;
         } else
             return sprite.texture;
     }

    public static int weightedRoll(List<int> rolls, int[] weights) {
        int[] finalW = new int[rolls.Count];
        int count = 0;
        int sum = 0;
        foreach(int x in rolls) {
            finalW[count] = sum+weights[x];
            sum+=weights[x];
            count++;
        }

        float random = Random.Range(0, sum);

        for(int i = finalW.Length-1; i >= 0; i--) {
            if(finalW[i]>=random) {
                return rolls[i];
            }
        }

        return -1;
    }

}
