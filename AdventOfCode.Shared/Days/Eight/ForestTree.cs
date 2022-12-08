namespace AdventOfCode.Shared.Days.Eight;

public class ForestTree
{
    private int Height { get; }
    
    public ForestTree? Top { get; set; }
    
    public ForestTree? Bottom { get; set; }

    public ForestTree? Left { get; set; }

    public ForestTree? Right { get; set; }

    public ForestTree(int height)
    {
        Height = height;
    }

    public bool IsVisible =>
        VisibleFromTop(Height) || VisibleFromLeft(Height) || VisibleFromBottom(Height) || VisibleFromRight(Height);
    
    public int ScenicScore => TopScore(Height) * LeftScore(Height) * BottomScore(Height) * RightScore(Height);
    
    private bool VisibleFromTop(int height) => Top == null || height > Top.Height && Top.VisibleFromTop(height);

    private bool VisibleFromLeft(int height) => Left == null || height > Left.Height && Left.VisibleFromLeft(height);
    
    private bool VisibleFromBottom(int height) => Bottom == null || height > Bottom.Height && Bottom.VisibleFromBottom(height);

    private bool VisibleFromRight(int height) => Right == null || height > Right.Height && Right.VisibleFromRight(height);
    
    private int TopScore(int height, int previousScore = 0) =>
        Top == null 
            ? previousScore 
            : height > Top.Height 
                ? Top.TopScore(height, previousScore + 1) 
                : previousScore + 1;

    private int LeftScore(int height, int previousScore = 0) =>
        Left == null 
            ? previousScore 
            : height > Left.Height 
                ? Left.LeftScore(height, previousScore + 1) 
                : previousScore + 1;
    
    private int BottomScore(int height, int previousScore = 0) =>
        Bottom == null 
            ? previousScore 
            : height > Bottom.Height 
                ? Bottom.BottomScore(height, previousScore + 1) 
                : previousScore + 1;
    
    private int RightScore(int height, int previousScore = 0) =>
        Right == null 
            ? previousScore 
            : height > Right.Height 
                ? Right.RightScore(height, previousScore + 1) 
                : previousScore + 1;
}
