using System;
namespace camp.model
{
    public class Score
    {
        public string ScoreId { get; set; }
        public string StudentId { get; }
        public string SubjectId { get; }
        public int Round { get; }
        public int Point { get; set; }
        public string Grade { get; set; }

        public Score(string studentId, string subjectId, int round, int point, string grade)
        {
            StudentId = studentId;
            SubjectId = subjectId;
            Round = round;
            Point = point;
            Grade = grade;
        }
    }

}

