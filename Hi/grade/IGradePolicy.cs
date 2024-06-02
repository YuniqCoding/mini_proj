using System;
namespace camp
{
    public interface IGradePolicy
    {
        string ConvertToGrade(int score);
    }
}

