using System;
namespace camp
{
    public class MandatoryPolicy : IGradePolicy
    {
        public string ConvertToGrade(int score)
        {
            if (score >= 95 && score <= 100)
            {
                return "A";
            }
            else if (score >= 90 && score <= 94)
            {
                return "B";
            }
            else if (score >= 80 && score <= 89)
            {
                return "C";
            }
            else if (score >= 70 && score <= 79)
            {
                return "D";
            }
            else if (score >= 60 && score <= 69)
            {
                return "F";
            }
            else
            {
                return "N";
            }
        }
    }
}

