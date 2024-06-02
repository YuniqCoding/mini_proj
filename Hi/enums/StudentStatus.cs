using System;
namespace camp.enums
{
    public enum StudentStatus
    {
        GREEN,
        RED,
        YELLOW
    }

    public static class StudentStatusExtensions
    {
        public static StudentStatus? FromString(string status)
        {
            foreach (StudentStatus value in Enum.GetValues(typeof(StudentStatus)))
            {
                if (value.ToString().Equals(status, StringComparison.OrdinalIgnoreCase))
                {
                    return value;
                }
            }

            throw new ArgumentNullException($"{status} 해당 상태는 존재하지 않습니다.");
        }
    }
}

