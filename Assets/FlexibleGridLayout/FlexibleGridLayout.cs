using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public int Rows;
    public int Columns;
    
    public Vector2 CellSize;
    public Vector2 Spacing;
    
    public FitTypes FitType;
    public Alignments Alignment;

    public bool FitX;
    public bool FitY;

    public enum Alignments
    {
        Horizontal,
        Vertical
    }

    public enum FitTypes
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (FitType == FitTypes.Width || FitType == FitTypes.Height || FitType == FitTypes.Uniform)
        {
            FitX = true;
            FitY = true;

            float sqrRt = Mathf.Sqrt(rectChildren.Count);

            Rows = Mathf.CeilToInt(sqrRt);
            Columns = Mathf.CeilToInt(sqrRt);
        }

        if(FitType == FitTypes.Width || FitType == FitTypes.FixedColumns)
        {
            Rows = Mathf.CeilToInt(rectChildren.Count / (float)Columns);
        }
        if (FitType == FitTypes.Height || FitType == FitTypes.FixedRows)
        {
            Columns = Mathf.CeilToInt(rectChildren.Count / (float)Rows);
        }


        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth;
        float cellHeight;

        if(Alignment == Alignments.Horizontal)
        {
            cellWidth = (parentWidth / (float)Columns) - ((Spacing.x / (float)Columns) * (Columns - 1)) - (padding.left / (float)Columns) - (padding.right / (float)Columns);
            cellHeight = (parentHeight / (float)Rows) - ((Spacing.y / (float)Rows) * (Rows - 1)) - (padding.top / (float)Rows) - (padding.bottom / (float)Rows);
        }
        else
        {
            cellHeight = (parentWidth / (float)Columns) - ((Spacing.x / (float)Columns) * (Columns - 1)) - (padding.left / (float)Columns) - (padding.right / (float)Columns);
            cellWidth = (parentHeight / (float)Rows) - ((Spacing.y / (float)Rows) * (Rows - 1)) - (padding.top / (float)Rows) - (padding.bottom / (float)Rows);
        }

        CellSize.x = FitX ? cellWidth : CellSize.x;
        CellSize.y = FitY ? cellHeight : CellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            var item = rectChildren[i];

            if (Alignment == Alignments.Horizontal)
            {
                rowCount = i / Columns;
                columnCount = i % Columns;
                var xPos = (CellSize.x * columnCount) + (Spacing.x * columnCount) + padding.left;
                var yPos = (CellSize.y * rowCount) + (Spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(item, 0, xPos, CellSize.x);
                SetChildAlongAxis(item, 1, yPos, CellSize.y);
            }
            else
            {
                rowCount = i / Rows;
                columnCount = i % Rows;
                var xPos = (CellSize.x * columnCount) + (Spacing.x * columnCount) + padding.left;
                var yPos = (CellSize.y * rowCount) + (Spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(item, 0, yPos, CellSize.y);
                SetChildAlongAxis(item, 1, xPos, CellSize.x);
            }
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
