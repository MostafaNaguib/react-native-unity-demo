using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerHighlighter : HighlighterController
{
    public bool randomColor;
    public Color highlightColor;
    
    public void Highlight(bool highlight)
    {
        if(randomColor)
        {
            h.ConstantOnImmediate(new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), highlight ? 1.0f : 0.0f));
        }
        else
        {
            h.ConstantOnImmediate(new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlight ? 1.0f : 0.0f));
        }

        
    }
}
